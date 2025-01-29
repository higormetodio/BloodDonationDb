using BloodDonationDb.Domain.Entities;
using BloodDonationDb.Domain.Enums;
using BloodDonationDb.Domain.ValueObjects;
using Bogus;

namespace CommomTestUtilities.Domain.Entities;
public class DonorBuilder
{
    public static Donor Builder()
    {
        return new Faker<Donor>()
            .CustomInstantiator(faker => new Donor(faker.Person.FullName, faker.Person.Email, faker.Person.DateOfBirth,
                faker.PickRandom<Gender>(), faker.Random.Number(140, 210), faker.PickRandom<BloodType>(), faker.PickRandom<RhFactor>(), new Address(
                    faker.Address.StreetAddress(),
                    faker.Address.BuildingNumber(),
                    faker.Address.City(),
                    faker.Address.State(),
                    faker.Address.ZipCode(),
                    faker.Address.Country())));
    }
}
