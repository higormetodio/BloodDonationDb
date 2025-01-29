using BloodDonationDb.Domain.Repositories.Donor;
using NSubstitute;

namespace CommomTestUtilities.Repositories.Donor;
public class DonorUpdateOnlyRepositoryBuilder
{
    public static IDonorUpdateOnlyRepository Builder()
        => Substitute.For<IDonorUpdateOnlyRepository>();
}
