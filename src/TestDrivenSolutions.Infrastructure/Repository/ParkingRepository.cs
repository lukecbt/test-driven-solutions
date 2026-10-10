using TestDrivenSolutions.Application.Interfaces;
using TestDrivenSolutions.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace TestDrivenSolutions.Infrastructure.Repository
{
    public class ParkingRepository(ApplicationDbContext context) : IParkingRepository
    {
        public async Task<(int, int)> GetSpaceCountsAsync()
        {
            int available = await context.Spaces.CountAsync(s => !s.IsOccupied);
            int occupied = await context.Spaces.CountAsync(s => s.IsOccupied);
            return (available, occupied);
        }

        public async Task<Space?> GetFirstAvailableSpaceAsync()
        {
            return await context.Spaces.OrderBy(s => s.Id).FirstOrDefaultAsync(s => !s.IsOccupied);
        }        
        
        public async Task<Space?> GetSpaceByVehicleRegAsync(string vehicleReg)
        {
            return await context.Spaces.FirstOrDefaultAsync(s => s.CurrentVehicleReg == vehicleReg);
        }

        public void UpdateSpace(Space space)
        {
            context.Spaces.Update(space);
        }

        public async Task<VehicleTypeTarrif?> GetCarTypeById(int id)
        {
            VehicleTypeTarrif? vehicleTypeTarrif = await context.VehicleTypeTarrifs.FirstOrDefaultAsync(ct => ct.Id == id);

            if (vehicleTypeTarrif is null)
            {
                // Assume that if no type found for specific vehicle type then default to small
                vehicleTypeTarrif = await context.VehicleTypeTarrifs.FirstOrDefaultAsync(ct => ct.Id == 1);
            }

            return vehicleTypeTarrif;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await context.SaveChangesAsync();
        }
    }
}
