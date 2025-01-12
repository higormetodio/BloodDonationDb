using BloodDonationDb.Domain.Enums;
using BloodDonationDb.Domain.Events;
using BloodDonationDb.Domain.ValueObjects;
using Bogus;

namespace CommomTestUtilities.Domain.Events;
public class BloodStockMinimumQuantityDomainEventBuilder
{
    public static BloodStockMinimumQuantityDomainEvent Builder()
    {
        return new Faker<BloodStockMinimumQuantityDomainEvent>()
            .RuleFor(b => b.BloodId, faker => faker.Random.Guid())
            .RuleFor(b => b.BloodType, faker => faker.PickRandom<BloodType>())
            .RuleFor(b => b.RhFactor, faker => faker.PickRandom<RhFactor>())
            .RuleFor(b => b.Quantity, faker => faker.Random.Number(0, BloodDonationRuleConstans.MINIMAL_QUANTITY_STOCK));
    }
}
