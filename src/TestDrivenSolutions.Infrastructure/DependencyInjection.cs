using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestDrivenSolutions.Application.Interfaces;
using TestDrivenSolutions.Infrastructure.Repository;

namespace TestDrivenSolutions.Infrastructure
{
    public static class DependencyInjection
    {
        // Services specfic to the infrasturcutr project
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Add PostgreSQL database
            services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            // Register repos
            services.AddScoped<IParkingRepository, ParkingRepository>();

            return services;
        }
    }
}
