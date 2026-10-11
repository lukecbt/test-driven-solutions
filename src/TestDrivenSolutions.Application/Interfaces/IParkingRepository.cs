using TestDrivenSolutions.Domain.Entities;

namespace TestDrivenSolutions.Application.Interfaces
{
    public interface IParkingRepository
    {
        Task<Space?> GetFirstAvailableSpaceAsync(CancellationToken ct = default);
        Task<Space?> GetSpaceByVehicleRegAsync(string vehicleReg, CancellationToken ct = default);
        Task<(int, int)> GetSpaceCountsAsync(CancellationToken ct = default);
        void UpdateSpace(Space space);
        Task<VehicleTypeTarrif?> GetCarTypeById(int id, CancellationToken ct = default);
        Task<int> SaveChangesAsync(CancellationToken ct = default);
    }
}
