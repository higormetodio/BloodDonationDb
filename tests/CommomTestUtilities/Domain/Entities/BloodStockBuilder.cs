using BloodDonationDb.Domain.Entities;
using BloodDonationDb.Domain.Enums;
using BloodDonationDb.Domain.ValueObjects;
using Bogus;

namespace CommomTestUtilities.Domain.Entities;
public class BloodStockBuilder
{
    public static BloodStock Builder()
    {
        return new Faker<BloodStock>()
            .CustomInstantiator(f => new BloodStock(f.PickRandom<BloodType>(), f.PickRandom<RhFactor>()));
    }
}
