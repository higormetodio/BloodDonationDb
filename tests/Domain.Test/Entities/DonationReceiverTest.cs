using BloodDonationDb.Domain.Entities;
using CommomTestUtilities.Domain.Entities;
using FluentAssertions;

namespace Domain.Test.Entities;
public class DonationReceiverTest
{
    private readonly DonationReceiver _donationReceiver = DonationReceiverBuilder.Builder();

    [Fact]
    public void Success_CreatedDonationReceiver()
    {
        _donationReceiver.Should().NotBeNull();
    }
}
