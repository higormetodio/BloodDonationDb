using BloodDonationDb.Domain.Entities;
using BloodDonationDb.Domain.Services.LoggedUser;
using CommomTestUtilities.Domain.Entities;
using NSubstitute;

namespace CommomTestUtilities.LoggedUser;
public class LoggedUserBuilder
{
    public static ILoggedUser Builder()
    {
        var (user, _) = UserBuilder.Builder();

        var mock = Substitute.For<ILoggedUser>();

        mock.User().Returns(Task.FromResult(user));

        return mock;
    }
}
