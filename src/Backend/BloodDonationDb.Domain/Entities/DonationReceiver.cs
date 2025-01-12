namespace BloodDonationDb.Domain.Entities;

public class DonationReceiver : Donation
{
    public DonationReceiver(Guid receiverId, Guid bloodStockId, DateTime when, int quantity) : base(when, quantity)
    {
        ReceiverId = receiverId;
        BloodStockId = bloodStockId;
    }
    
    protected DonationReceiver(){ }

    public Guid ReceiverId { get; private set; }
    public Receiver? Receiver { get; private set; }
    public Guid BloodStockId { get; private set; }
    public BloodStock? BloodStock { get; private set; }
}