using Microsoft.IdentityModel.Tokens;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace BloodDonationDb.Infrastructure.Security.Tokens.Access;
public abstract class JwtTokenHandler
{
    protected static SymmetricSecurityKey SecurityKey(string signingKey)
    {
        var bytes = Encoding.UTF8.GetBytes(signingKey);        

        return new SymmetricSecurityKey(bytes);
    }
}
