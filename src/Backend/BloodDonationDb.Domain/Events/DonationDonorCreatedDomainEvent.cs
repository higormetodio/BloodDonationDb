using BloodDonationDb.Domain.Enums;
using BloodDonationDb.Domain.SeedWorks;

namespace BloodDonationDb.Domain.Events;

public record DonationDonorCreatedDomainEvent(
    Guid DonationDonorId,
    string Name,
    string Email,
    BloodType BloodType,
    RhFactor RhFactor,
    int Quantity) : IDomainEvent;