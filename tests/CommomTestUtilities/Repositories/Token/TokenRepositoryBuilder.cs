using BloodDonationDb.Domain.Entities;
using BloodDonationDb.Domain.Repositories.Token;
using NSubstitute;

namespace CommomTestUtilities.Repositories.Token;
public class TokenRepositoryBuilder
{
    private readonly ITokenRepository _repository;

    public TokenRepositoryBuilder() => _repository = Substitute.For<ITokenRepository>();

    public TokenRepositoryBuilder Get(RefreshToken refreshToken)
    {
        if (refreshToken is not null)
        {
            _repository.Get(refreshToken.Value!)!.Returns(Task.FromResult(refreshToken));
        }

        return this;
    }

    public ITokenRepository Build() => _repository;
}
