using BloodDonationDb.Domain.Entities;
using CommomTestUtilities.Domain.Entities;
using FluentAssertions;

namespace Domain.Test.Entities;
public class DonationDonorTest
{
    private readonly DonationDonor _donationDonor = DonationDonorBuilder.Builder();
    [Fact]
    public void Success_CreatedDonationDonor()
    {
        _donationDonor.Should().NotBeNull();
    }
}
