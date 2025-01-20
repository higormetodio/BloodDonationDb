using BloodDonationDb.Domain.Entities;
using BloodDonationDb.Domain.Security.Tokens;
using BloodDonationDb.Domain.Services.LoggedUser;
using BloodDonationDb.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BloodDonationDb.Infrastructure.Services.LoggedUser;
public class LoggedUser : ILoggedUser
{
    private readonly BloodDonationDbContext _dbContext;
    private readonly ITokenProvider _tokenProvider;

    public LoggedUser(BloodDonationDbContext dbContext, ITokenProvider tokenProvider)
    {
        _dbContext = dbContext;
        _tokenProvider = tokenProvider;
    }

    public async Task<User> User()
    {
        var token = _tokenProvider.Value();

        var tokenHandler = new JwtSecurityTokenHandler();

        var jwtSecurityToken = tokenHandler.ReadJwtToken(token);

        var identifier = jwtSecurityToken.Claims.First(c => c.Type == ClaimTypes.Sid).Value;

        var userIdentifier = Guid.Parse(identifier);

        return await _dbContext
            .Users
            .AsNoTracking()
            .FirstAsync(user => user.Active && user.Id == userIdentifier);
    }
}
