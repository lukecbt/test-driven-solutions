using TestDrivenSolutions.Application.Helpers;
using TestDrivenSolutions.Application.Interfaces;
using TestDrivenSolutions.Application.Models.Request;
using TestDrivenSolutions.Application.Models.Response;
using TestDrivenSolutions.Domain.Entities;

namespace TestDrivenSolutions.Application.Services
{
    public class ParkingService(IParkingRepository repository) : IParkingService
    {
        // <inheritdoc/>
        public async Task<VehicleParkingResponse?> AddParking(VehicleParkingRequest request, CancellationToken ct)
        {
            Space? space = await repository.GetSpaceByVehicleRegAsync(request.VehicleReg, ct);

            if (space is not null)
            {
                // Vehicle already parked, handle error client side
                return null;
            }

            space = await repository.GetFirstAvailableSpaceAsync(ct);

            if (space is null)
            {
                // No spaces available, handle error client side
                return null;
            }

            DateTime timeIn = DateTime.UtcNow;

            // Update space information
            space.CurrentVehicleReg = request.VehicleReg;
            space.CurrentVehicleType = request.VehicleType;
            space.IsOccupied = true;
            space.TimeIn = timeIn;

            repository.UpdateSpace(space);
            await repository.SaveChangesAsync(ct);

            // Return successfully parked car information
            return new()
            {
                VehicleReg = request.VehicleReg,
                SpaceNumber = space.Id,
                TimeIn = timeIn
            };
        }

        // <inheritdoc/>
        public async Task<ParkingResponse> GetAvailableAndOccupiedSpaces(CancellationToken ct)
        {
            var (available, occupied) = await repository.GetSpaceCountsAsync(ct);

            return new()
            {
                AvailableSpaces = available,
                OccupiedSpaces = occupied
            };
        }

        // <inheritdoc/>
        public async Task<VehicleExitResponse?> RemoveParking(VehicleExitRequest request, CancellationToken ct)
        {
            Space? space = await repository.GetSpaceByVehicleRegAsync(request.VehicleReg, ct);

            if (space is null || space.TimeIn is null || space.CurrentVehicleType is null)
            {
                // A catch all for a car that is not already parked, handle error client side
                return null;
            }

            // Calculate parking charge
            DateTime timeIn = space.TimeIn.Value;
            DateTime timeOut = DateTime.UtcNow;
            int vehicleTypeId = space.CurrentVehicleType.Value;
            VehicleTypeTarrif? vehicleTypeTarrif = await repository.GetCarTypeById(vehicleTypeId, ct);

            // Cacluation parking charge, add catch all if database is not set up, we need to calculate based on base charge
            decimal parkingCharge = CalculationHelper.CalculateParkingCharge(timeIn, timeOut, vehicleTypeTarrif?.CostPerMinute ?? 0.1m, vehicleTypeTarrif?.AdditionalChargeMinutes ?? 5, vehicleTypeTarrif?.AdditionalChargeAmount ?? 1);

            // Update space information to available
            space.CurrentVehicleReg = null;
            space.CurrentVehicleType = null;
            space.IsOccupied = false;
            space.TimeIn = null;

            repository.UpdateSpace(space);
            await repository.SaveChangesAsync(ct);

            return new()
            {
                VehicleReg = request.VehicleReg,
                VehicleCharge = parkingCharge,
                TimeIn = timeIn,
                TimeOut = timeOut
            };
        }
    }

}