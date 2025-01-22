using BloodDonationDb.Domain.Entities;
using BloodDonationDb.Domain.Repositories.Donor;
using NSubstitute;

namespace CommomTestUtilities.Repositories.Donor;
public class DonorWriteOnlyRepositoryBuilder
{
    public static IDonorWriteOnlyRepository Builder()
        => Substitute.For<IDonorWriteOnlyRepository>();
}
