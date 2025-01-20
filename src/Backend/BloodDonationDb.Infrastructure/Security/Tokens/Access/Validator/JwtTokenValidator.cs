using BloodDonationDb.Domain.Security.Tokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BloodDonationDb.Infrastructure.Security.Tokens.Access.Validator;
public class JwtTokenValidator : JwtTokenHandler, IAccessTokenValidator
{
    private readonly string _signinKey;

    public JwtTokenValidator(string signinKey)
    {
        _signinKey = signinKey;
    }

    public Guid ValidateAndGetUserIdentifier(string token)
    {
        var validationParameter = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            IssuerSigningKey = SecurityKey(_signinKey),
            ClockSkew = TimeSpan.Zero
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var principal = tokenHandler.ValidateToken(token, validationParameter, out _);

        var userIdentifier = principal.Claims.First(c => c.Type == ClaimTypes.Sid).Value;

        return Guid.Parse(userIdentifier);
    }
}
