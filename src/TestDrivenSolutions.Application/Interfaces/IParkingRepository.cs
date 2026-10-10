using TestDrivenSolutions.Domain.Entities;

namespace TestDrivenSolutions.Application.Interfaces
{
    public interface IParkingRepository
    {
        Task<Space?> GetFirstAvailableSpaceAsync();
        Task<Space?> GetSpaceByVehicleRegAsync(string vehicleReg);
        Task<(int, int)> GetSpaceCountsAsync();
        void UpdateSpace(Space space);
        Task<int> SaveChangesAsync();
    }
}
