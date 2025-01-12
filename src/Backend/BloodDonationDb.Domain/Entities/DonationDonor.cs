namespace BloodDonationDb.Domain.Entities;

public class DonationDonor : Donation
{
    public DonationDonor(Guid donorId, Guid bloodStockId, DateTime when, int quantity) : base(when, quantity)
    {
        DonorId = donorId;
        BloodStockId = bloodStockId;
    }
    
    protected DonationDonor(){ }

    public Guid DonorId { get; private set; }
    public Donor? Donor { get; private set; }
    public Guid BloodStockId { get; private set; }
    public BloodStock? BloodStock { get; set; }
}