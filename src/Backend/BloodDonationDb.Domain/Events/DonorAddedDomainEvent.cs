using BloodDonationDb.Domain.Enums;
using BloodDonationDb.Domain.SeedWorks;
using BloodDonationDb.Domain.ValueObjects;

namespace BloodDonationDb.Domain.Events;

public record DonorAddedDomainEvent(
    string Name,
    string Email,
    DateTime BirthDate,
    Gender Gender,
    int Weight,
    BloodType BloodType,
    RhFactor RhFactor,
    Address Address
    ) : IDomainEvent;