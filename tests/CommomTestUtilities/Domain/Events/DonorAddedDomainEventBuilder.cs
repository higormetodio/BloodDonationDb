using BloodDonationDb.Domain.Events;
using Bogus;
using CommomTestUtilities.Domain.Entities;

namespace CommomTestUtilities.Domain.Events;
public class DonorAddedDomainEventBuilder
{
    public static DonorAddedDomainEvent Builder()
    {
        var donor = DonorBuilder.Builder();

        return new Faker<DonorAddedDomainEvent>()
            .RuleFor(d => d.Name, faker => donor.Name)
            .RuleFor(d => d.Email, faker => donor.Email)
            .RuleFor(d => d.BirthDate, faker => donor.BirthDate)
            .RuleFor(d => d.Gender, faker => donor.Gender)
            .RuleFor(d => d.Weight, faker => donor.Weight)
            .RuleFor(d => d.BloodType, faker => donor.BloodType)
            .RuleFor(d => d.RhFactor, faker => donor.RhFactor)
            .RuleFor(d => d.Address, faker => donor.Address);
    }
}
