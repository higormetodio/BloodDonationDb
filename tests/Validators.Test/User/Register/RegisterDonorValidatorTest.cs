using BloodDonationDb.Application.Commands.User.Register;
using BloodDonationDb.Exceptions;
using CommomTestUtilities.Commands;
using FluentAssertions;

namespace Validators.Test.User.Register;
public class RegisterDonorValidatorTest
{
    [Fact]
    public void Success()
    {
        var validator = new RegisterUserValidator();

        var command = RegisterUserCommandBuilder.Builder();

        var result = validator.Validate(command);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Error_Name_Empty()
    {
        var validator = new RegisterUserValidator();

        var command = RegisterUserCommandBuilder.Builder();
        command.Name = string.Empty;

        var result = validator.Validate(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceMessageException.NAME_EMPTY));
    }

    [Fact]
    public void Error_Email_Empty()
    {
        var validator = new RegisterUserValidator();

        var command = RegisterUserCommandBuilder.Builder();
        command.Email = string.Empty;

        var result = validator.Validate(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceMessageException.EMAIL_EMPTY));
    }

    [Fact]
    public void Error_Email_Invalid()
    {
        var validator = new RegisterUserValidator();

        var command = RegisterUserCommandBuilder.Builder();
        command.Email = "email.com.br";

        var result = validator.Validate(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.ContainSingle(e => e.ErrorMessage.Equals(ResourceMessageException.EMAIL_INVALID));
    }

    [Fact]
    public void Error_Password_Empty()
    {
        var validator = new RegisterUserValidator();

        var command = RegisterUserCommandBuilder.Builder();
        command.Password = string.Empty;

        var result = validator.Validate(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.ContainSingle(e => e.ErrorMessage.Equals(ResourceMessageException.PASSWORD_EMPTY));
    }

    [Theory]
    [InlineData("1")]
    [InlineData("12")]
    [InlineData("12@")]
    [InlineData("12@3")]
    [InlineData("12@34")]
    [InlineData("12345")]
    [InlineData("123456@")]
    [InlineData("12345678")]
    [InlineData("12345678@")]
    [InlineData("12345678@9")]
    public void Error_Password_Invalid(string password)
    {
        var validator = new RegisterUserValidator();

        var command = RegisterUserCommandBuilder.Builder();
        command.Password = password;

        var result = validator.Validate(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.ContainSingle(e => e.ErrorMessage.Equals(ResourceMessageException.PASSWORD_INVALID));
    }
}
