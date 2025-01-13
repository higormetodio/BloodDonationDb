using BloodDonationDb.Domain.Entities;
using CommomTestUtilities.Domain.Entities;
using FluentAssertions;

namespace Domain.Test.Entities;
public class UserTest
{
    private readonly User _user = UserBuilder.Builder();

    [Fact]
    public void Success_CreatedUser()
    {
        _user.Should().NotBeNull();
        _user.Name.Should().NotBeNullOrEmpty();
        _user.Email.Should().NotBeNullOrEmpty();
        _user.Password.Should().NotBeNullOrEmpty();
    }
}
