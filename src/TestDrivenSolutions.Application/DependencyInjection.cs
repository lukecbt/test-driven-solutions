using Microsoft.Extensions.DependencyInjection;
using TestDrivenSolutions.Application.Interfaces;
using TestDrivenSolutions.Application.Services;

namespace TestDrivenSolutions.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IParkingService, ParkingService>();
            return services;
        }
    }
}
