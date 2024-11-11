using BloodDonationDb.Domain.Enums;

namespace BloodDonationDb.Domain.Entities;

public class Donor : BaseEntity
{
    public Donor(string name, string email, DateTime birthDate, Gender gender, int weight, int bloodId)
    {
        Name = name;
        Email = email;
        BirthDate = birthDate;
        Gender = gender;
        Weight = weight;
        IsDonor = CanBeADonor(birthDate);
        BloodId = bloodId;
    }
    
    public string Name { get; private set; }
    public string Email { get; private set; }
    public DateTime BirthDate { get; private set; }
    public Gender Gender { get; private set; }
    public int Weight { get; private set; }
    public bool IsDonor { get; private set; }
    public DateTime LastDonation { get; private set; }
    public DateTime NextDonation { get; private set; }
    public int BloodId { get; private set; }
    public Blood Blood { get; private set; }

    public Address Address { get; private set; }

    public IEnumerable<Donation> Donations { get; private set; }

    public void UpdateDonor(string name, string email, DateTime birthDate, Gender gender, int weight, int bloodId)
    {
        Name = name;
        Email = email;
        BirthDate = birthDate;
        Gender = gender;
        Weight = weight;
        BloodId = bloodId;
    }

    public bool CanBeADonor(DateTime birthDate)
    {
        var age = DateTime.Now.Year - birthDate.Year;

        if (birthDate.AddYears(age) > DateTime.Now)
        {
            age--;
        }

        if (age >= 18)
        {
            return true;
        }

        return false;
    }

    public void UpdateLastDonation(DateTime lastDonation)
    {
        LastDonation = lastDonation;

        NextDonation = lastDonation.AddDays(Gender == Gender.Female ? 90 : 60);
    }
}