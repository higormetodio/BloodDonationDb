using BloodDonationDb.Domain.Enums;
using BloodDonationDb.Domain.ValueObjects;

namespace BloodDonationDb.Application.Models.Donor;
public class DonorViewModel
{
    public DonorViewModel(Guid donorId, string? name, string? email, DateTime birthDate, Gender gender, 
        int weight, bool isDonor, DateTime? lastDonation, DateTime? nextDonation, BloodType bloodType, 
        RhFactor rhFactor, Address? address, bool active)
    {
        DonorId = donorId;
        Name = name;
        Email = email;
        BirthDate = birthDate;
        Gender = gender;
        Weight = weight;
        IsDonor = isDonor;
        LastDonation = lastDonation;
        NextDonation = nextDonation;
        BloodType = bloodType;
        RhFactor = rhFactor;
        Address = address;
        Active = active;
    }

    public Guid DonorId { get; private set; }
    public string? Name { get; private set; }
    public string? Email { get; private set; }
    public DateTime BirthDate { get; private set; }
    public Gender Gender { get; private set; }
    public int Weight { get; private set; }
    public bool IsDonor { get; private set; }
    public DateTime? LastDonation { get; private set; }
    public DateTime? NextDonation { get; private set; }
    public BloodType BloodType { get; private set; }
    public RhFactor RhFactor { get; private set; }
    public Address? Address { get; private set; }
    public bool Active { get; private set; }

    public static DonorViewModel FromEntity(Domain.Entities.Donor entity)
        => new(entity.Id, entity.Name, entity.Email, entity.BirthDate, entity.Gender, entity.Weight, entity.IsDonor, entity.LastDonation, entity.NextDonation, entity.BloodType, entity.RhFactor, entity.Address, entity.Active);
}
