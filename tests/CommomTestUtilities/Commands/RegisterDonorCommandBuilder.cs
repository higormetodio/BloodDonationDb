using BloodDonationDb.Application.Commands.Donor.Register;
using BloodDonationDb.Domain.Entities;
using BloodDonationDb.Domain.Enums;
using BloodDonationDb.Domain.Repositories.Donor;
using BloodDonationDb.Domain.ValueObjects;
using Bogus;
using System.IO;
using System.Reflection.Emit;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CommomTestUtilities.Commands;
public class RegisterDonorCommandBuilder
{
    public static RegisterDonorCommand Builder()
    {
        return new Faker<RegisterDonorCommand>()
            .RuleFor(donor => donor.Name, faker => faker.Person.FirstName)
            .RuleFor(donor => donor.Email, (f, donor) => f.Internet.Email(donor.Email))
            .RuleFor(donor => donor.BirthDate, faker => faker.Date.Between(new DateTime(DateTime.UtcNow.Year - 68, 1, 1), new DateTime(DateTime.UtcNow.Year - 1, 1, 1)))
            .RuleFor(donor => donor.Gender, faker => faker.PickRandom<Gender>())
            .RuleFor(donor => donor.Weight, faker => faker.Random.Int(50, 140))
            .RuleFor(donor => donor.BloodType, faker => faker.PickRandom<BloodType>())
            .RuleFor(donor => donor.RhFactor, faker => faker.PickRandom<RhFactor>())
            .RuleFor(donor => donor.Address, new Faker<Address>()
                .CustomInstantiator(faker => new Address
                (
                    faker.Address.StreetAddress(),
                    faker.Address.BuildingNumber(),
                    faker.Address.City(),
                    faker.Address.State(),
                    faker.Address.ZipCode(),
                    faker.Address.Country()
                )));
    }
}
