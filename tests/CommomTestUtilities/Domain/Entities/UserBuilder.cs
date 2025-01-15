using BloodDonationDb.Domain.Entities;
using Bogus;
using CommomTestUtilities.Criptography;

namespace CommomTestUtilities.Domain.Entities;
public class UserBuilder
{
    public static (User user, string password) Builder()
    {
        var passwordEncripter = PasswordEncripterBuilder.Builder();

        var password = new Faker().Internet.Password();

        var user = new Faker<User>()
            .CustomInstantiator(faker => new User(faker.Person.FullName, faker.Person.Email, passwordEncripter.Encript(password)));

        return (user, password);
    }
}
