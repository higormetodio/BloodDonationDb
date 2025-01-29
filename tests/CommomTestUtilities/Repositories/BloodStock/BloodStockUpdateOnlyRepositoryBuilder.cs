using BloodDonationDb.Domain.Repositories.BloodStock;
using NSubstitute;

namespace CommomTestUtilities.Repositories.BloodStock;
public class BloodStockUpdateOnlyRepositoryBuilder
{
    public static IBloodStockUpdateOnlyRepository Builder()
        => Substitute.For<IBloodStockUpdateOnlyRepository>();
}
