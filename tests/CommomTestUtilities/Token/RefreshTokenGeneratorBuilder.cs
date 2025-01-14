using BloodDonationDb.Domain.Security.Tokens;
using BloodDonationDb.Infrastructure.Security.Tokens.Refresh;

namespace CommomTestUtilities.Token;
public class RefreshTokenGeneratorBuilder
{
    public static IRefreshTokenGenerator Build() => new RefreshTokenGenerator();
}
