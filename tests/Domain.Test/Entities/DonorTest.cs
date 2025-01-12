using BloodDonationDb.Domain.Entities;
using BloodDonationDb.Domain.Enums;
using CommomTestUtilities.Domain.Entities;
using FluentAssertions;

namespace Domain.Test.Entities;
public class DonorTest
{
    private readonly Donor _donor = DonorBuilder.Builder();

    [Fact]
    public void Success_CreatedDonor()
    {
        _donor.Should().NotBeNull();
        _donor.Name.Should().NotBeNullOrEmpty();
        _donor.Email.Should().NotBeNullOrEmpty();
        _donor.BirthDate.Should().NotBe(default(DateTime));
        _donor.Gender.Should().BeOneOf(Gender.Male, Gender.Female);
        _donor.Weight.Should().BeInRange(140, 210);
        _donor.BloodType.Should().BeOneOf([BloodType.A, BloodType.B, BloodType.O, BloodType.AB]);
        _donor.RhFactor.Should().BeOneOf([RhFactor.Positive, RhFactor.Negative]);
        _donor.Address.Should().NotBeNull();
        _donor.Active.Should().BeTrue();
        _donor.Donations.Should().BeNullOrEmpty();
        _donor.LastDonation.Should().Be(default(DateTime));
        _donor.NextDonation.Should().Be(default(DateTime));

    }

    [Fact]
    public void Success_CanBeADonor_Is_True()
    {
        var newDonor = new Donor(_donor.Name!, _donor.Email!, new DateTime(1979, 12, 4), _donor.Gender, _donor.Weight, _donor.BloodType, _donor.RhFactor, _donor.Address!);
        
        newDonor.IsDonor.Should().BeTrue();
    }

    [Fact]
    public void Success_CanBeADonor_Is_False()
    {
        var newDonor = new Donor(_donor.Name!, _donor.Email!, new DateTime(2008, 12, 4), _donor.Gender, _donor.Weight, _donor.BloodType, _donor.RhFactor, _donor.Address!);

        newDonor.IsDonor.Should().BeFalse();
    }
}
