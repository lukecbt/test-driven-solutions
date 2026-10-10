using TestDrivenSolutions.Application.Models.Request;
using TestDrivenSolutions.Application.Models.Response;

namespace TestDrivenSolutions.Application.Interfaces
{
    public interface IParkingService
    {
        /// <summary>
        /// Add a vehicle to first available space
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Vehcle reg, parking space number and time parked</returns>
        Task<VehicleParkingResponse> AddParking(VehicleParkingRequest request);
        /// <summary>
        /// Get all available and occupied spaces
        /// </summary>
        /// <returns>Count of available and occupied spaces</returns>
        Task<IEnumerable<ParkingResponse>> GetAvailableAndOccupiedSpaces();
        /// <summary>
        /// Remove a vehicle from an occupied space by vehicle reg
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Vehicle reg, parking charge and time parked</returns>
        Task<VehicleExitResponse> RemoveParking(VehicleExitRequest request);
    }
}