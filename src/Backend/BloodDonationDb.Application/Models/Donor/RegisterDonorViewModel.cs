using BloodDonationDb.Domain.Entities;
using BloodDonationDb.Domain.Enums;
using MongoDB.Driver;

namespace BloodDonationDb.Application.Models.Donor;
public class RegisterDonorViewModel
{
    public RegisterDonorViewModel(string? name, string? email, BloodType bloodType, RhFactor rhFactor)
    {
        Name = name;
        Email = email;
        BloodType = bloodType;
        RhFactor = rhFactor;
    }

    public string? Name { get; private set; }
    public string? Email { get; private set; }
    public BloodType BloodType { get; private set; }
    public RhFactor RhFactor { get; private set; }

    public static RegisterDonorViewModel FromEntity(Domain.Entities.Donor entity)
        => new(entity.Name, entity.Email, entity.BloodType, entity.RhFactor);
}
