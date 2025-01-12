using BloodDonationDb.Domain.Entities;
using CommomTestUtilities.Domain.Entities;
using FluentAssertions;

namespace Domain.Test.Entities;
public class ReceiverTest
{
    private readonly Receiver _receiver = ReceiverBuilder.Builder();

    [Fact]
    public void Success_CreatedReceiver()
    {
        _receiver.Should().NotBeNull();
        _receiver.Name.Should().NotBeNullOrEmpty();
        _receiver.Email.Should().NotBeNullOrEmpty();    
        _receiver.Address.Should().NotBeNull();
    }
}
