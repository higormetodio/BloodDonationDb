using BloodDonationDb.Domain.Repositories.Donor;
using NSubstitute;

namespace CommomTestUtilities.Repositories.Donor;
public class DonorReadOnlyRepositoryBuilder
{
    private readonly IDonorReadOnlyRepository _repository = Substitute.For<IDonorReadOnlyRepository>();

    public void ExistActiveDonorWithEmail(string email)
    {
        _repository.ExistActiveDonorWithEmail(email).Returns(Task.FromResult(true));
    }

    public IDonorReadOnlyRepository Builder() => _repository;


}
