using BloodDonationDb.Domain.Entities;
using Bogus;

namespace CommomTestUtilities.Domain.Entities;
public class UserBuilder
{
    public static User Builder()
    {
        return new Faker<User>()
            .CustomInstantiator(faker => new User(faker.Person.FullName, faker.Person.Email, faker.Internet.Email()));
    }
}
