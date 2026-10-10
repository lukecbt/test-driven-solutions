using TestDrivenSolutions.Application.Models.Request;
using TestDrivenSolutions.Application.Models.Response;

namespace TestDrivenSolutions.Application.Interfaces
{
    public interface IParkingService
    {
        Task<VehicleParkingResponse> AddParking(VehicleParkingRequest request);
        Task<IEnumerable<ParkingResponse>> GetParking();
        Task<VehicleExitResponse> RemoveParking(VehicleExitRequest request);
    }
}