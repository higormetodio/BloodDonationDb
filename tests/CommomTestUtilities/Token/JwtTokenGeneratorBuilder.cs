using BloodDonationDb.Domain.Security.Tokens;
using BloodDonationDb.Infrastructure.Security.Tokens.Access.Generator;
using NSubstitute;

namespace CommomTestUtilities.Token;
public class JwtTokenGeneratorBuilder
{
    public static IAccessTokenGenerator Builder() => new JwtTokenGenerator(expirationTimeMinutes: 5, signingKey: "666c5ce52faa35efa70fa610f72203e93fa960aa316ef13584dc80b6898f17f5");
}
