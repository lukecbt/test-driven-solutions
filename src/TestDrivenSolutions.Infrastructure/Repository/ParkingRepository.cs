using TestDrivenSolutions.Application.Interfaces;
using TestDrivenSolutions.Domain.Entities;

namespace TestDrivenSolutions.Infrastructure.Repository
{
    public class ParkingRepository(ApplicationDbContext context) : IParkingRepository
    {
        // TODO: Data access logic
        public Task<Space> AddParkingAsync(Space space)
        {
            throw new NotImplementedException();
        }

        public Task<Space> GetFirstAvailableSpaceAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Space>> GetSpacesAsync()
        {
            throw new NotImplementedException();
        }

        public Task RemoveParkingAsync(Space space)
        {
            throw new NotImplementedException();
        }

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
