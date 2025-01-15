using BloodDonationDb.Domain.Entities;
using CommomTestUtilities.Domain.Entities;
using FluentAssertions;

namespace Domain.Test.Entities;
public class UserTest
{
    [Fact]
    public void Success_CreatedUser()
    {
        var (user, password) = UserBuilder.Builder();

        user.Should().NotBeNull();
        user.Name.Should().NotBeNullOrEmpty();
        user.Email.Should().NotBeNullOrEmpty();
        password.Should().NotBeNullOrEmpty();
    }
}
