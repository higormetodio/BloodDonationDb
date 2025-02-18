using BloodDonationDb.Domain.Entities;

namespace BloodDonationDb.Application.Models.DonationReceiver;
public class RegisterDonationReceiverViewModel
{
    public RegisterDonationReceiverViewModel(string name, string blood, int quantity, DateTime donatioDate)
    {
        Name = name;
        Blood = blood;
        Quantity = quantity;
        DonatioDate = donatioDate;
    }

    public string Name { get; private set; }
    public string Blood { get; private set; }
    public int Quantity { get; private set; }
    public DateTime DonatioDate { get; private set; }

    public static RegisterDonationReceiverViewModel FromEntity(
        Domain.Entities.Receiver receiver, 
        BloodStock bloodStock,
        Domain .Entities.DonationReceiver donation)
        => new(receiver.Name!, $"{bloodStock.BloodType} {bloodStock.RhFactor}", donation.Quantity, donation.When);
}
