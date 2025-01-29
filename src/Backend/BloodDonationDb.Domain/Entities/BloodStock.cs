using BloodDonationDb.Domain.Enums;
using BloodDonationDb.Domain.SeedWorks;
using BloodDonationDb.Domain.ValueObjects;

namespace BloodDonationDb.Domain.Entities;

public class BloodStock : Entity, IAggregateRoot
{
    public BloodStock(BloodType bloodType, RhFactor rhFactor)
    {
        BloodType = bloodType;
        RhFactor = rhFactor;
        Quantity = BloodDonationRuleConstants.INITIAL_QUANTITY_STOCK;
        MinimumQuantityReached = true;
        DonationDonors = [];
        DonationReceivers = [];

    }
    
    protected BloodStock() { }

    public BloodType BloodType { get; private set; }
    public RhFactor RhFactor { get; private set; }
    public int Quantity { get; private set; }
    public bool MinimumQuantityReached { get; private set; }
    
    public IEnumerable<DonationDonor>? DonationDonors { get; private set; }
    public IEnumerable<DonationReceiver>? DonationReceivers { get; private set; }


    public void UpdateStockDonationDonor(int quantity)
    {
        Quantity += quantity;
        IsMinimumQuantityReached();
    }

    public void UpdateStockDonationReceiver(int quantity)
    {
        Quantity -= quantity;
        IsMinimumQuantityReached();
    }
    
    private void IsMinimumQuantityReached()
    {
        MinimumQuantityReached = Quantity <= BloodDonationRuleConstants.MINIMAL_QUANTITY_STOCK;
    }
}