using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestDrivenSolutions.Infrastructure;

namespace TestDrivenSolutions.Tests.IntegrationTests
{
    /// <summary>
    /// Build custom factory for running tests with a database connection
    /// </summary>
    public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Remove default db context connection
                var dbContextDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));
                services.Remove(dbContextDescriptor);

                // Set up test db overide
                var sp = services.BuildServiceProvider();
                var configuration = sp.GetRequiredService<IConfiguration>();
                services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("TestConnection")));

                // Initialise database with migrations and seed data before each test
                using var scope = services.BuildServiceProvider().CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                db.Database.EnsureDeleted();
                db.Database.Migrate();
            });

            builder.UseEnvironment("Development");
        }
    }
}
