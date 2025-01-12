using BloodDonationDb.Domain.Enums;
using BloodDonationDb.Domain.SeedWorks;

namespace BloodDonationDb.Domain.Events;

public record BloodStockMinimumQuantityDomainEvent(
    Guid BloodId,
    BloodType BloodType,
    RhFactor RhFactor,
    int Quantity) : IDomainEvent;