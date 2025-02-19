using BloodDonationDb.Domain.Enums;
using BloodDonationDb.Domain.SeedWorks;
using BloodDonationDb.Domain.ValueObjects;

namespace BloodDonationDb.Domain.Entities;

public class Donor : AggregateRoot
{
    public Donor(string name, string email, DateTime birthDate, Gender gender, int weight, BloodType bloodType, RhFactor rhFactor, Address address)
    {
        Name = name;
        Email = email;
        BirthDate = birthDate;
        Gender = gender;
        Weight = weight;
        IsDonor = CanBeADonor(birthDate);
        BloodType = bloodType;
        RhFactor = rhFactor;
        Address = address;
        Active = true;
        Donations = [];
    }
    
    protected Donor(){ }
    
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

    public IEnumerable<DonationDonor>? Donations { get; private set; }

    private bool CanBeADonor(DateTime birthDate)
    {
        var age = DateTime.Now.Year - birthDate.Year;

        if (birthDate.AddYears(age) > DateTime.UtcNow)
        {
            age--;
        }

        return age >= BloodDonationRuleConstants.AGE_ALLOWED_DONATION;
    }

    public void UpdateLastDonation(DateTime lastDonation)
    {
        LastDonation = lastDonation.Date;

        NextDonation = lastDonation.Date.AddDays(Gender == Gender.Female ? BloodDonationRuleConstants.DAYS_NEXT_DONATION_WOMAN : BloodDonationRuleConstants.DAYS_NEXT_DONATION_MAN);
    }
    
    public void ToInactive()
    {
        Active = false;
    }
}