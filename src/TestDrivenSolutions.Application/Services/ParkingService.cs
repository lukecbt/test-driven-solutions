using TestDrivenSolutions.Application.Interfaces;
using TestDrivenSolutions.Application.Models.Request;
using TestDrivenSolutions.Application.Models.Response;

namespace TestDrivenSolutions.Application.Services
{
    public class ParkingService(IParkingRepository repository) : IParkingService
    {
        public Task<VehicleParkingResponse> AddParking(VehicleParkingRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ParkingResponse>> GetParking()
        {
            throw new NotImplementedException();
        }

        public Task<VehicleExitResponse> RemoveParking(VehicleExitRequest request)
        {
            throw new NotImplementedException();
        }
    }

}