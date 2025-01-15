using BloodDonationDb.Domain.Repositories.User;
using NSubstitute;

namespace CommomTestUtilities.Repositories.User;
public class UserReadOnlyRepositoryBuilder
{
    private readonly IUserReadOnlyRepository _repository = Substitute.For<IUserReadOnlyRepository>();

    public void GetByEmail(BloodDonationDb.Domain.Entities.User user)
    {
        _repository.GetByEmailAsync(user.Email)!.Returns(Task.FromResult(user));
    }

    public IUserReadOnlyRepository Builder() => _repository;
}
