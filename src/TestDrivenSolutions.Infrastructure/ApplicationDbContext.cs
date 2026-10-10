using Microsoft.EntityFrameworkCore;
using TestDrivenSolutions.Domain.Entities;

namespace TestDrivenSolutions.Infrastructure
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        // Entities
        public DbSet<Space> Spaces { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<VehicleType> VehicleTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Seed the database with spaces and types

            // Assume there is 20 spaces (this could maybe be an environment variable later)
            List<Space> spaces = new();
            for (int i = 0; i < 20; i++)
            {
                spaces.Add(new() { Id = i+1, IsOccupied = false, CurrentVehicleReg = null, CurrentVehicleType = null, TimeIn = null });
            }
            builder.Entity<Space>().HasData(spaces);
            // Index to search by current car reg
            builder.Entity<Space>().HasIndex(s => s.CurrentVehicleReg);

            builder.Entity<VehicleType>().HasData(
                new VehicleType { Id = 1, CostPerMinute = 0.10m, Description = "Small car" },
                new VehicleType { Id = 2, CostPerMinute = 0.20m, Description = "Medium car" },
                new VehicleType { Id = 3, CostPerMinute = 0.40m, Description = "Large car" }
            );
        }
    }
}
