using BloodDonationDb.Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace WebAPI.Test;
public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Test")
            .ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<BloodDonationDbContext>));

                if (descriptor is not null)
                {
                    services.Remove(descriptor);
                }

                var provaider = services.AddEntityFrameworkInMemoryDatabase()
                    .BuildServiceProvider();

                services.AddDbContext<BloodDonationDbContext>(options =>
                {
                    options.UseInMemoryDatabase("BloodDonationDbInMemoryTest");
                    options.UseInternalServiceProvider(provaider);
                });
            });
    }
}
