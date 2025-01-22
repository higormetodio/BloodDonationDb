using BloodDonationDb.Application.Commands.Donor.Register;
using BloodDonationDb.Exceptions;
using CommomTestUtilities.Commands;
using FluentAssertions;

namespace Validators.Test.Donor.Register;
public class RegisterDonorValidatorTest
{
    [Fact]
    public void Success()
    {
        var valiator = new RegisterDonorValidator();

        var command = RegisterDonorCommandBuilder.Builder();

        var result = valiator.Validate(command);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Error_Name_Empty()
    {
        var valiator = new RegisterDonorValidator();

        var command = RegisterDonorCommandBuilder.Builder();
        command.Name = string.Empty;

        var result = valiator.Validate(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceMessageException.NAME_EMPTY));
    }

    [Fact]
    public void Error_Email_Empty()
    {
        var valiator = new RegisterDonorValidator();

        var command = RegisterDonorCommandBuilder.Builder();
        command.Email = string.Empty;

        var result = valiator.Validate(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceMessageException.EMAIL_EMPTY));
    }

    [Fact]
    public void Error_Email_Invalid()
    {
        var valiator = new RegisterDonorValidator();

        var command = RegisterDonorCommandBuilder.Builder();
        command.Email = "email.com.br";

        var result = valiator.Validate(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceMessageException.EMAIL_INVALID));
    }

    [Fact]
    public void Error_Birth_Date_Empty()
    {
        var valiator = new RegisterDonorValidator();

        var command = RegisterDonorCommandBuilder.Builder();
        command.BirthDate = DateTime.MinValue;

        var result = valiator.Validate(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceMessageException.BIRTH_DATE_EMPTY));
    }

    [Fact]
    public void Error_Birth_Date_Invalid()
    {
        var valiator = new RegisterDonorValidator();

        var command = RegisterDonorCommandBuilder.Builder();
        command.BirthDate = new DateTime(2027, 7, 7);

        var result = valiator.Validate(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceMessageException.BIRTH_DATE_INVALID));
    }

    [Theory]
    [InlineData("1956-12-31")]
    [InlineData("1956-01-01")]
    [InlineData("1950-07-09")]
    public void Error_Birth_Date_Not_Allowed(string birthDate)
    {
        var valiator = new RegisterDonorValidator();

        var command = RegisterDonorCommandBuilder.Builder();
        command.BirthDate = DateTime.Parse(birthDate);

        var result = valiator.Validate(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceMessageException.BIRTH_DATE_NOT_ALLOWED));
    }

    [Fact]
    public void Error_Gender_Not_Supported()
    {
        var valiator = new RegisterDonorValidator();

        var command = RegisterDonorCommandBuilder.Builder();
        command.Gender = (BloodDonationDb.Domain.Enums.Gender)100;

        var result = valiator.Validate(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceMessageException.GENDER_NOT_SUPPORTED));
    }

    [Theory]
    [InlineData(49)]
    [InlineData(141)]
    public void Error_Weight_Not_Allowed(int weight)
    {
        var valiator = new RegisterDonorValidator();

        var command = RegisterDonorCommandBuilder.Builder();
        command.Weight = weight;

        var result = valiator.Validate(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceMessageException.WEIGHT_NOT_ALLOWED));
    }

    [Fact]
    public void Error_Blood_Type_Not_Supported()
    {
        var valiator = new RegisterDonorValidator();

        var command = RegisterDonorCommandBuilder.Builder();
        command.BloodType = (BloodDonationDb.Domain.Enums.BloodType)100;

        var result = valiator.Validate(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceMessageException.BLOOD_TYPE_NOT_SUPPOTED));
    }

    [Fact]
    public void Error_Rh_Factor_Not_Supported()
    {
        var valiator = new RegisterDonorValidator();

        var command = RegisterDonorCommandBuilder.Builder();
        command.RhFactor = (BloodDonationDb.Domain.Enums.RhFactor)100;

        var result = valiator.Validate(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceMessageException.RH_FACTOR_NOT_SUPPORTED));
    }
}
