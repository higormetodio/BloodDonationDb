using Microsoft.Extensions.Configuration;

namespace BloodDonationDb.Infrastructure.Extensions;

public static class ConfigurationExtension
{
    public static string ConnectionStringSql(this IConfiguration configuration)
    {
        return configuration.GetConnectionString("BloodDonationDb")!;
    }
}