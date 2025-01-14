using BloodDonationDb.Domain.Entities;

namespace BloodDonationDb.Domain.Repositories.Token;
public interface ITokenRepository
{
    Task<RefreshToken?> Get(string refreshToken);

    Task SaveNewRefreshToken(RefreshToken refreshToken);
}
