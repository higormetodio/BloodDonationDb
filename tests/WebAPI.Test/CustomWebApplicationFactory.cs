using BloodDonationDb.Infrastructure.Persistence;
using CommomTestUtilities.Domain.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace WebAPI.Test;
public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    private BloodDonationDb.Domain.Entities.User _user = default;
    private string _password = string.Empty;

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

                using var scope = services.BuildServiceProvider().CreateScope();

                var context = scope.ServiceProvider.GetRequiredService<BloodDonationDbContext>();

                context.Database.EnsureDeleted();

                StartDatabase(context);
            });
    }

    public string GetEmail() => _user.Email;

    public string GetPassword() => _password;   

    public string GetName() => _user.Name;

    public Guid GetUserId() => _user.Id;

    private void StartDatabase(BloodDonationDbContext context)
    {
        (_user, _password) = UserBuilder.Builder();

        context.Users.Add(_user);

        context.SaveChanges();
    }
}
