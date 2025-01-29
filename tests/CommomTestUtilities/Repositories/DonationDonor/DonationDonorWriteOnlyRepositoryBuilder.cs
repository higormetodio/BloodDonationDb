using BloodDonationDb.Domain.Repositories.DonationDonor;
using NSubstitute;

namespace CommomTestUtilities.Repositories.DonationDonor;
public class DonationDonorWriteOnlyRepositoryBuilder
{   
    public static IDonationDonorWriteOnlyRepository Builder() => Substitute.For<IDonationDonorWriteOnlyRepository>();
}
