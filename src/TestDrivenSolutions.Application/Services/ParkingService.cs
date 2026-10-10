using TestDrivenSolutions.Application.Interfaces;
using TestDrivenSolutions.Application.Models.Request;
using TestDrivenSolutions.Application.Models.Response;
using TestDrivenSolutions.Domain.Entities;

namespace TestDrivenSolutions.Application.Services
{
    public class ParkingService(IParkingRepository repository) : IParkingService
    {
        // <inheritdoc/>
        public async Task<VehicleParkingResponse?> AddParking(VehicleParkingRequest request)
        {
            Space? space = await repository.GetFirstAvailableSpaceAsync();

            if (space is null)
            {
                // No spaces available handle error client side
                return null;
            }

            DateTime timeIn = DateTime.UtcNow;

            // Update space information
            space.CurrentVehicleReg = request.VehicleReg;
            space.CurrentVehicleType = request.VehicleType;
            space.IsOccupied = true;
            space.TimeIn = timeIn;

            repository.UpdateSpace(space);
            await repository.SaveChangesAsync();

            // Return successfully parked car information
            return new()
            {
                VehicleReg = request.VehicleReg,
                SpaceNumber = space.Id,
                TimeIn = timeIn
            };
        }

        // <inheritdoc/>
        public async Task<ParkingResponse> GetAvailableAndOccupiedSpaces()
        {
            var (available, occupied) = await repository.GetSpaceCountsAsync();

            return new()
            {
                AvailableSpaces = available,
                OccupiedSpaces = occupied
            };
        }

        // <inheritdoc/>
        public Task<VehicleExitResponse> RemoveParking(VehicleExitRequest request)
        {
            throw new NotImplementedException();
        }
    }

}