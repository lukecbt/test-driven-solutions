using TestDrivenSolutions.Application.Interfaces;
using TestDrivenSolutions.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace TestDrivenSolutions.Infrastructure.Repository
{
    public class ParkingRepository(ApplicationDbContext context) : IParkingRepository
    {
        public async Task<(int, int)> GetSpaceCountsAsync(CancellationToken ct = default)
        {
            int available = await context.Spaces.CountAsync(s => !s.IsOccupied, ct);
            int occupied = await context.Spaces.CountAsync(s => s.IsOccupied, ct);
            return (available, occupied);
        }

        public async Task<Space?> GetFirstAvailableSpaceAsync(CancellationToken ct = default)
        {
            return await context.Spaces.OrderBy(s => s.Id).FirstOrDefaultAsync(s => !s.IsOccupied, ct);
        }        
        
        public async Task<Space?> GetSpaceByVehicleRegAsync(string vehicleReg, CancellationToken ct = default)
        {
            return await context.Spaces.FirstOrDefaultAsync(s => s.CurrentVehicleReg == vehicleReg, ct);
        }

        public void UpdateSpace(Space space)
        {
            context.Spaces.Update(space);
        }

        public async Task<VehicleTypeTarrif?> GetCarTypeById(int id, CancellationToken ct = default)
        {
            VehicleTypeTarrif? vehicleTypeTarrif = await context.VehicleTypeTarrifs.FirstOrDefaultAsync(ct => ct.Id == id, ct);

            if (vehicleTypeTarrif is null)
            {
                // Assume that if no type found for specific vehicle type then default to small
                vehicleTypeTarrif = await context.VehicleTypeTarrifs.FirstOrDefaultAsync(ct => ct.Id == 1, ct);
            }

            return vehicleTypeTarrif;
        }

        public async Task<int> SaveChangesAsync(CancellationToken ct = default)
        {
            return await context.SaveChangesAsync(ct);
        }
    }
}
