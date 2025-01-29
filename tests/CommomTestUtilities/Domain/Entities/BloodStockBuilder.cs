using BloodDonationDb.Domain.Entities;
using BloodDonationDb.Domain.Enums;
using BloodDonationDb.Domain.ValueObjects;
using Bogus;

namespace CommomTestUtilities.Domain.Entities;
public class BloodStockBuilder
{
    public static BloodStock Builder(Object obj)
    {
        if (obj.GetType().Equals(typeof(Receiver)))
        {
            var receiver = (Receiver)obj;

            return new Faker<BloodStock>()
            .CustomInstantiator(f => new BloodStock(receiver!.BloodType, receiver.RhFactor));
        }

        var donor = (Donor)obj;

        return new Faker<BloodStock>()
        .CustomInstantiator(f => new BloodStock(donor!.BloodType, donor.RhFactor));



    }
}
