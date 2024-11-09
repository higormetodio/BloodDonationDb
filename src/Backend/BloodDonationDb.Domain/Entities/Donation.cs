namespace BloodDonationDb.Domain.Entities;

public class Donation : BaseEntity
{
    public Donation(int donorId, DateTime donationDate, int quantity)
    {
        DonorId = donorId;
        DonationDate = donationDate;
        Quantity = quantity;
    }

    public int DonorId { get; private set; }
    public Donor? Donor { get; private set; }
    public DateTime DonationDate { get; private set; }
    public int Quantity { get; private set; }

    public void UpdateDonation(int donorId, DateTime donationDate, int quantity)
    {
        DonorId = donorId;
        DonationDate = donationDate;
        Quantity = quantity;
    }
    
}