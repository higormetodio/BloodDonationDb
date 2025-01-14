using BloodDonationDb.Domain.Entities;
using BloodDonationDb.Domain.Repositories.Token;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonationDb.Infrastructure.Persistence.Repositories;
public class TokenRepository : ITokenRepository
{
    private readonly BloodDonationDbContext _context;

    public TokenRepository(BloodDonationDbContext context) => _context = context;

    public async Task<RefreshToken?> Get(string refreshToken)
    {
        return await _context
            .RefreshTokens
            .AsNoTracking()
            .Include(token => token.User)
            .FirstOrDefaultAsync(token => token!.Value!.Equals(refreshToken));
    }

    public async Task SaveNewRefreshToken(RefreshToken refreshToken)
    {
        var tokens = _context.RefreshTokens.Where(token => token.UserId == refreshToken.UserId);

        _context.RefreshTokens.RemoveRange(tokens);

        await _context.RefreshTokens.AddAsync(refreshToken);
    }
}
