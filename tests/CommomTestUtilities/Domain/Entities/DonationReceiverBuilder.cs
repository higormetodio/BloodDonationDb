using BloodDonationDb.Domain.Entities;
using Bogus;

namespace CommomTestUtilities.Domain.Entities;
public class DonationReceiverBuilder
{
    public static DonationReceiver Builder()
    {
        var receiver = ReceiverBuilder.Builder();
        var bloodStock = BloodStockBuilder.Builder();

        return new Faker<DonationReceiver>()
            .CustomInstantiator(faker => new DonationReceiver(receiver.Id, bloodStock.Id,
                faker.Date.Between(DateTime.Now.AddDays(faker.Random.Number(-5, 0)), DateTime.Now),
                faker.Random.Number(1, bloodStock.Quantity)))
            .RuleFor(d => d.Receiver, faker => receiver);
    }
}
