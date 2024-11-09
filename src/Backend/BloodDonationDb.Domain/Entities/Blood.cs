using BloodDonationDb.Domain.Enums;

namespace BloodDonationDb.Domain.Entities;

public class Blood : BaseEntity
{
    public Blood(BloodType bloodType, RhFactor rhFactor)
    {
        BloodType = bloodType;
        RhFactor = rhFactor;
        Stock = new Stock(Id, 0);
    }

    public BloodType BloodType { get; private set; }
    public RhFactor RhFactor { get; private set; }
    public Stock Stock { get; private set; }
    
    public IEnumerable<Donor>? Donors { get; private set; }
}