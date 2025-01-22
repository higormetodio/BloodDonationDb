using BloodDonationDb.Domain.Entities;
using BloodDonationDb.Domain.Enums;
using BloodDonationDb.Domain.ValueObjects;
using CommomTestUtilities.Domain.Entities;
using FluentAssertions;

namespace Domain.Test.Entities;
public class BloodStockTest
{
    private readonly BloodStock _bloodStock = BloodStockBuilder.Builder();

    [Fact]
    public void Success_Created_BloodStock()
    {
        _bloodStock.Should().NotBeNull();
        _bloodStock.BloodType.Should().BeOneOf([BloodType.A, BloodType.B, BloodType.O, BloodType.AB]);
        _bloodStock.RhFactor.Should().BeOneOf([RhFactor.Positive, RhFactor.Negative]);
        _bloodStock.Quantity.Should().Be(BloodDonationRuleConstants.INITIAL_QUANTITY_STOCK);
        _bloodStock.MinimumQuantityReached.Should().BeTrue();
        _bloodStock.DonationDonors.Should().NotBeNull();
        _bloodStock.DonationReceivers.Should().NotBeNull();
    }

    [Fact]
    public void Success_Update_Quantity_When_To_Call_UpdateStockDonation_Method()
    {
        _bloodStock.UpdateStockDonation(420);

        _bloodStock.Quantity.Should().Be(420);
    }

    [Fact]
    public void Success_Update_Quantity_When_To_Call_UpdateStockReceived_Method()
    {
        _bloodStock.UpdateStockDonation(420);
        _bloodStock.UpdateStockReceived(420);

        _bloodStock.Quantity.Should().Be(0);
    }

    [Fact]
    public void Success_Update_False_MinimumQuantityReached_When_To_Call_IsMinimumQuantityReached()
    {
        _bloodStock.UpdateStockDonation(4500);

        _bloodStock.MinimumQuantityReached.Should().BeFalse();
    }
}
