using TestDrivenSolutions.Domain.Entities;

namespace TestDrivenSolutions.Application.Interfaces
{
    public interface IParkingRepository
    {
        Task<Space> GetFirstAvailableSpaceAsync();
        Task<IEnumerable<Space>> GetAllSpacesAsync();
        Task<Space> AddParkingAsync(Space space);
        Task RemoveParkingAsync(Space space);
        Task SaveChangesAsync();
    }
}
