using BloodDonationDb.Domain.Entities;

namespace BloodDonationDb.Application.Models.DonorDonation;
public class RegisterDonationDonorViewModel
{
    public RegisterDonationDonorViewModel(string name, string blood, int quantity, DateTime donatioDate)
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

    public static RegisterDonationDonorViewModel FromEntity(Domain.Entities.Donor donor, BloodStock bloodStock, DonationDonor donation)
        => new(donor.Name!, $"{bloodStock.BloodType} {bloodStock.RhFactor}", donation.Quantity, donation.When);
}
