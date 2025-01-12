using BloodDonationDb.Domain.Entities;
using BloodDonationDb.Domain.ValueObjects;
using Bogus;

namespace CommomTestUtilities.Domain.Entities;
public class ReceiverBuilder
{
    public static Receiver Builder()
    {
        return new Faker<Receiver>()
            .CustomInstantiator(faker => new Receiver(faker.Person.FullName, faker.Person.Email, new Address(
                faker.Address.StreetAddress(),
                faker.Address.BuildingNumber(),
                faker.Address.City(),
                faker.Address.State(),
                faker.Address.ZipCode(),
                faker.Address.Country()
            )));
    }
}
