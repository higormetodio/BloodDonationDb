using BloodDonationDb.Application.Commands.DonationDonor.Register;
using BloodDonationDb.Exceptions;
using CommomTestUtilities.Commands;
using FluentAssertions;

namespace Validators.Test.DonationDonor;
public class RegisterDonationDonorValidatorTest
{
    [Fact]
    public void Success()
    {
        var validator = new RegisterDonationDonorValidator();

        var registerDonoCommand = RegisterDonorCommandBuilder.Builder();

        var command = RegisterDonationDonorCommandBuilder.Builder(registerDonoCommand);

        var result = validator.Validate(command);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Error_Donation_Date_Empty()
    {
        var validator = new RegisterDonationDonorValidator();

        var registerDonoCommand = RegisterDonorCommandBuilder.Builder();

        var command = RegisterDonationDonorCommandBuilder.Builder(registerDonoCommand);
        command.DonationDate = DateTime.MinValue;

        var result = validator.Validate(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceMessageException.DONATION_DATE_EMPTY));
    }

    [Theory]
    [InlineData(400)]
    [InlineData(500)]
    public void Error_Quantity_Empty(int quantity)
    {
        var validator = new RegisterDonationDonorValidator();

        var registerDonoCommand = RegisterDonorCommandBuilder.Builder();

        var command = RegisterDonationDonorCommandBuilder.Builder(registerDonoCommand);
        command.Quantity = quantity;

        var result = validator.Validate(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceMessageException.DONATION_QUANTITY_NOT_ALLOWED));
    }
    
    [Fact]
    public void Error_Donation_Date_Invalid()
    {
        var validator = new RegisterDonationDonorValidator();

        var registerDonoCommand = RegisterDonorCommandBuilder.Builder();

        var command = RegisterDonationDonorCommandBuilder.Builder(registerDonoCommand);
        command.DonationDate = command.DonationDate.AddDays(10);

        var result = validator.Validate(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceMessageException.DONATION_DATE_GREATER_CURRENT_DATE));
    }
}
