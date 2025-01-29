using BloodDonationDb.Application.Commands.Donor.Register;
using BloodDonationDb.Application.Commands.DonationDonor.Register;
using Bogus;

namespace CommomTestUtilities.Commands;
public class RegisterDonationDonorCommandBuilder
{
    public static RegisterDonationDonorCommand Builder(RegisterDonorCommand? registerDonorCommand)
    {       
        return new Faker<RegisterDonationDonorCommand>()
            .RuleFor(donation => donation.Email, faker => registerDonorCommand!.Email)
            .RuleFor(donation => donation.DonationDate, faker => faker.Date.Between(DateTime.Now.AddDays(-10), DateTime.Now))
            .RuleFor(donation => donation.Quantity, faker => faker.Random.Int(420, 470));
    }
}
