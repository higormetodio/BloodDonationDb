using BloodDonationDb.Domain.Entities;
using Bogus;

namespace CommomTestUtilities.Domain.Entities;
public class DonationDonorBuilder
{
    public static DonationDonor Builder()
    {
        var donnor = DonorBuilder.Builder();
        var bloodStock = BloodStockBuilder.Builder();

        return new Faker<DonationDonor>()
            .CustomInstantiator(faker => new DonationDonor(donnor.Id, bloodStock.Id, faker.Date.Between(DateTime.Now.AddDays(faker.Random.Number(-5, 0)), DateTime.Now), faker.Random.Number(420, 470)))
            .RuleFor(d => d.Donor, faker => donnor);
    }
}
