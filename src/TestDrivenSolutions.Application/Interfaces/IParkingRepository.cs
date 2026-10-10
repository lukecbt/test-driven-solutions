using TestDrivenSolutions.Domain.Entities;

namespace TestDrivenSolutions.Application.Interfaces
{
    public interface IParkingRepository
    {
        Task<Space?> GetFirstAvailableSpaceAsync();
        Task<(int, int)> GetSpaceCounts();
        void UpdateSpace(Space space);
        Task<int> SaveChangesAsync();
    }
}
