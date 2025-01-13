using BloodDonationDb.Domain.SeedWorks;
using NSubstitute;

namespace CommomTestUtilities.Repositories;
public class UnitOfWorkBuilder
{
    public static IUnitOfWork Builder()
    {
        var mock = Substitute.For<IUnitOfWork>();

        return mock;
    }
}
