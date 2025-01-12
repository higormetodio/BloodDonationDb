using BloodDonationDb.Domain.Events;
using Bogus;
using CommomTestUtilities.Domain.Entities;

namespace CommomTestUtilities.Domain.Events;
public class DonationReceiverCreatedDomainEventBuilder
{
    public static DonationReceiverCreatedDomainEvent Builder()
    {
        var donationReceiver = DonationReceiverBuilder.Builder();

        return new Faker<DonationReceiverCreatedDomainEvent>()
            .RuleFor(d => d.DonationReceiverId, faker => donationReceiver.Id)
            .RuleFor(d => d.Name, faker => donationReceiver.Receiver!.Name)
            .RuleFor(d => d.Email, faker => donationReceiver.Receiver!.Email)
            .RuleFor(d => d.BloodType, faker => donationReceiver.BloodStock!.BloodType)
            .RuleFor(Builder => Builder.RhFactor, faker => donationReceiver.BloodStock!.RhFactor)
            .RuleFor(d => d.Quantity, faker => donationReceiver.Quantity);
    }
}
