using BloodDonationDb.Application.Commands.Donor.Register;
using BloodDonationDb.Domain.Entities;
using BloodDonationDb.Exceptions;
using CommomTestUtilities.Commands;
using CommomTestUtilities.Token;
using FluentAssertions;
using NSubstitute;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Net;
using System.Text.Json;
using WebAPI.Test.InlineData;

namespace WebAPI.Test.DonationDonor;
public class RegisterDonationDonorTest : BloodDonationDbClassFixture
{
    public const string METHOD = "donationdonor";
    public readonly Guid _userId;
    public readonly BloodDonationDb.Domain.Entities.Donor _donor;

    public RegisterDonationDonorTest(CustomWebApplicationFactory factory) : base(factory)
    {
        _userId = factory.GetUserId();
        _donor = factory.GetDonor();
    }

    [Fact]
    public async Task Success()
    {
        var registerDonorCommand = new RegisterDonorCommand
        {
            Name = _donor.Name,
            Email = _donor.Email,
            BirthDate = _donor.BirthDate,
            Gender = _donor.Gender,
            Weight = _donor.Weight,
            BloodType = _donor.BloodType,
            RhFactor = _donor.RhFactor,
            Address = _donor.Address          
        };

        var command = RegisterDonationDonorCommandBuilder.Builder(registerDonorCommand);

        var token = JwtTokenGeneratorBuilder.Builder().Generate(_userId);

        var response = await PostAsync(method: METHOD, command: command, token);

        response.StatusCode.Should().Be(HttpStatusCode.Created);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        responseData.RootElement.GetProperty("name").GetString().Should().Be(_donor.Name);
        responseData.RootElement.GetProperty("blood").GetString().Should().Be($"{_donor.BloodType} {_donor.RhFactor}");
        responseData.RootElement.GetProperty("quantity").GetInt32().Should().Be(command.Quantity);
    }

    [Theory]
    [ClassData(typeof(CultureInlineDataTest))]
    public async Task Error_Donor_Not_Found(string culture)
    {
        var registerDonorCommand = new RegisterDonorCommand
        {
            Name = _donor.Name,
            Email = "invalidemail@email.com",
            BirthDate = _donor.BirthDate,
            Gender = _donor.Gender,
            Weight = _donor.Weight,
            BloodType = _donor.BloodType,
            RhFactor = _donor.RhFactor,
            Address = _donor.Address
        };

        var command = RegisterDonationDonorCommandBuilder.Builder(registerDonorCommand);

        var token = JwtTokenGeneratorBuilder.Builder().Generate(_userId);

        var response = await PostAsync(method: METHOD, command: command, token, culture: culture);

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        var errors = responseData.RootElement.GetProperty("errors").EnumerateArray();

        var expectedMessage = ResourceMessageException.ResourceManager.GetString("DONOR_NOT_FOUND", new CultureInfo(culture));

        errors.Should().HaveCount(1).And.Contain(e => e.GetString()!.Equals(expectedMessage));
    }

    [Theory]
    [ClassData(typeof(CultureInlineDataTest))]
    public async Task Error_Donation_Date_Empty(string culture)
    {
        var registerDonorCommand = new RegisterDonorCommand
        {
            Name = _donor.Name,
            Email = _donor.Email,
            BirthDate = _donor.BirthDate,
            Gender = _donor.Gender,
            Weight = _donor.Weight,
            BloodType = _donor.BloodType,
            RhFactor = _donor.RhFactor,
            Address = _donor.Address
        };

        var command = RegisterDonationDonorCommandBuilder.Builder(registerDonorCommand);
        command.DonationDate = DateTime.MinValue;

        var token = JwtTokenGeneratorBuilder.Builder().Generate(_userId);

        var response = await PostAsync(method: METHOD, command: command, token, culture: culture);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        var errors = responseData.RootElement.GetProperty("errors").EnumerateArray();

        var expectedMessage = ResourceMessageException.ResourceManager.GetString("DONATION_DATE_EMPTY", new CultureInfo(culture));

        errors.Should().HaveCount(1).And.Contain(e => e.GetString()!.Equals(expectedMessage));
    }

    [Theory]
    [ClassData(typeof(CultureInlineDataTest))]
    public async Task Error_Donation_GREATER_Current_Date(string culture)
    {
        var registerDonorCommand = new RegisterDonorCommand
        {
            Name = _donor.Name,
            Email = _donor.Email,
            BirthDate = _donor.BirthDate,
            Gender = _donor.Gender,
            Weight = _donor.Weight,
            BloodType = _donor.BloodType,
            RhFactor = _donor.RhFactor,
            Address = _donor.Address
        };

        var command = RegisterDonationDonorCommandBuilder.Builder(registerDonorCommand);
        command.DonationDate = DateTime.UtcNow.AddDays(10);

        var token = JwtTokenGeneratorBuilder.Builder().Generate(_userId);

        var response = await PostAsync(method: METHOD, command: command, token, culture: culture);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        var errors = responseData.RootElement.GetProperty("errors").EnumerateArray();

        var expectedMessage = ResourceMessageException.ResourceManager.GetString("DONATION_DATE_GREATER_CURRENT_DATE", new CultureInfo(culture));

        errors.Should().HaveCount(1).And.Contain(e => e.GetString()!.Equals(expectedMessage));
    }
}
