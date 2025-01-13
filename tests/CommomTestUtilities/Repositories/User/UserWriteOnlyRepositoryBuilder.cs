using BloodDonationDb.Domain.Repositories.User;
using NSubstitute;

namespace CommomTestUtilities.Repositories.User;
public class UserWriteOnlyRepositoryBuilder
{
    public static IUserWriteOnlyRepository Builder()
    {
        var mock = Substitute.For<IUserWriteOnlyRepository>();

        return mock;
    }
}
