using BloodDonationDb.Domain.SeedWorks;

namespace BloodDonationDb.Domain.Entities;

public abstract class Donation : AggregateRoot
{
    protected Donation(DateTime when, int quantity)
    {
        When = when;
        Quantity = quantity;
    }
    
    protected Donation(){ }
    
    public DateTime When { get; private set; }
    public int Quantity { get; private set; }
}