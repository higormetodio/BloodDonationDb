using BloodDonationDb.Domain.Entities;
using BloodDonationDb.Domain.Events;
using Bogus;
using CommomTestUtilities.Domain.Entities;

namespace CommomTestUtilities.Domain.Events;
public class DonationDonorCreatedDomainEventBuilder
{
    public static DonationDonorCreatedDomainEvent Builder()
    {
        var donationDonor = DonationDonorBuilder.Builder();

        return new Faker<DonationDonorCreatedDomainEvent>()
            .RuleFor(d => d.DonationDonorId, faker => donationDonor.Id)
            .RuleFor(d => d.Name, faker => donationDonor.Donor!.Name)
            .RuleFor(d => d.Email, faker => donationDonor.Donor!.Email)
            .RuleFor(d => d.BloodType, faker => donationDonor.Donor!.BloodType)
            .RuleFor(Builder => Builder.RhFactor, faker => donationDonor.Donor!.RhFactor)
            .RuleFor(d => d.Quantity, faker => donationDonor.Quantity);
    }
}
