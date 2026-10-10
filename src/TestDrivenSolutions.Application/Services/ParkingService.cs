using TestDrivenSolutions.Application.Interfaces;
using TestDrivenSolutions.Application.Models.Request;
using TestDrivenSolutions.Application.Models.Response;

namespace TestDrivenSolutions.Application.Services
{
    public class ParkingService(IParkingRepository repository) : IParkingService
    {
        // <inheritdoc/>
        public Task<VehicleParkingResponse> AddParking(VehicleParkingRequest request)
        {
            throw new NotImplementedException();
        }

        // <inheritdoc/>
        public Task<IEnumerable<ParkingResponse>> GetAvailableAndOccupiedSpaces()
        {
            throw new NotImplementedException();
        }

        // <inheritdoc/>
        public Task<VehicleExitResponse> RemoveParking(VehicleExitRequest request)
        {
            throw new NotImplementedException();
        }
    }

}