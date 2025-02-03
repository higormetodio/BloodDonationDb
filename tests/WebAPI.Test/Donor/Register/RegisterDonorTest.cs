using BloodDonationDb.Application.DTOs;
using BloodDonationDb.Exceptions;
using CommomTestUtilities.Commands;
using CommomTestUtilities.Token;
using FluentAssertions;
using System.Globalization;
using System.Net;
using System.Text.Json;
using System.Text.RegularExpressions;
using WebAPI.Test.InlineData;

namespace WebAPI.Test.Donor.Register;
public class RegisterDonorTest : BloodDonationDbClassFixture
{
    public const string METHOD = "donor";
    public readonly Guid _userId;
    public RegisterDonorTest(CustomWebApplicationFactory factory) : base(factory)
    {
        _userId = factory.GetUserId();
    }

    [Fact]
    public async Task Success()
    {
        var command = RegisterDonorCommandBuilder.Builder();

        command.Address = new BloodDonationDb.Domain.ValueObjects.Address(
            "",
            "123",
            "",
            "",
            "41815050",
            "Brasil");

        var token = JwtTokenGeneratorBuilder.Builder().Generate(_userId);

        var response = await PostAsync(method: METHOD, command: command, token: token);

        response.StatusCode.Should().Be(HttpStatusCode.Created);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        responseData.RootElement.GetProperty("donorId").GetString().Should().NotBeNullOrWhiteSpace();
        responseData.RootElement.GetProperty("name").GetString().Should().Be(command.Name);
    }

    [Theory]
    [ClassData(typeof(CultureInlineDataTest))]
    public async Task Error_CEP_Not_Found(string culture)
    {
        var command = RegisterDonorCommandBuilder.Builder();

        command.Address = new BloodDonationDb.Domain.ValueObjects.Address(
            "",
            "123",
            "",
            "",
            "4180000000",
            "Brasil");

        var token = JwtTokenGeneratorBuilder.Builder().Generate(_userId);

        var response = await PostAsync(method: METHOD, command: command, token: token, culture);

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        var errors = responseData.RootElement.GetProperty("errors").EnumerateArray();

        var expectedMessage = ResourceMessageException.ResourceManager.GetString("CEP_NOT_FOUND", new CultureInfo(culture));

        errors.Should().HaveCount(1).And.Contain(e => e.GetString()!.Equals(expectedMessage));
    }

    [Theory]
    [ClassData(typeof(CultureInlineDataTest))]
    public async Task Error_Name_Empy(string culture)
    {
        var command = RegisterDonorCommandBuilder.Builder();
        command.Name = string.Empty;
        command.Address = new BloodDonationDb.Domain.ValueObjects.Address(
            "",
            "123",
            "",
            "",
            "41815050",
            "Brasil");

        var token = JwtTokenGeneratorBuilder.Builder().Generate(_userId);

        var response = await PostAsync(method: METHOD, command: command, token: token, culture: culture);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        var errors = responseData.RootElement.GetProperty("errors").EnumerateArray();

        var expectedMessage = ResourceMessageException.ResourceManager.GetString("NAME_EMPTY", new CultureInfo(culture));

        errors.Should().HaveCount(1).And.Contain(e => e.GetString()!.Equals(expectedMessage));
    }

    [Theory]
    [ClassData(typeof(CultureInlineDataTest))]
    public async Task Error_Email_Empy(string culture)
    {
        var command = RegisterDonorCommandBuilder.Builder();
        command.Email = string.Empty;
        command.Address = new BloodDonationDb.Domain.ValueObjects.Address(
            "",
            "123",
            "",
            "",
            "41815050",
            "Brasil");

        var token = JwtTokenGeneratorBuilder.Builder().Generate(_userId);

        var response = await PostAsync(method: METHOD, command: command, token: token, culture: culture);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        var errors = responseData.RootElement.GetProperty("errors").EnumerateArray();

        var expectedMessage = ResourceMessageException.ResourceManager.GetString("EMAIL_EMPTY", new CultureInfo(culture));

        errors.Should().HaveCount(1).And.Contain(e => e.GetString()!.Equals(expectedMessage));
    }

    [Theory]
    [ClassData(typeof(CultureInlineDataTest))]
    public async Task Error_Email_Invalid(string culture)
    {
        var command = RegisterDonorCommandBuilder.Builder();
        command.Email = "email.com.br";
        command.Address = new BloodDonationDb.Domain.ValueObjects.Address(
            "",
            "123",
            "",
            "",
            "41815050",
            "Brasil");

        var token = JwtTokenGeneratorBuilder.Builder().Generate(_userId);

        var response = await PostAsync(method: METHOD, command: command, token: token, culture: culture);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        var errors = responseData.RootElement.GetProperty("errors").EnumerateArray();

        var expectedMessage = ResourceMessageException.ResourceManager.GetString("EMAIL_INVALID", new CultureInfo(culture));

        errors.Should().HaveCount(1).And.Contain(e => e.GetString()!.Equals(expectedMessage));
    }

    [Theory]
    [ClassData(typeof(CultureInlineDataTest))]
    public async Task Error_Birth_Date_Empty(string culture)
    {
        var command = RegisterDonorCommandBuilder.Builder();
        command.BirthDate = DateTime.MinValue;
        command.Address = new BloodDonationDb.Domain.ValueObjects.Address(
            "",
            "123",
            "",
            "",
            "41815050",
            "Brasil");

        var token = JwtTokenGeneratorBuilder.Builder().Generate(_userId);

        var response = await PostAsync(method: METHOD, command: command, token: token, culture: culture);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        var errors = responseData.RootElement.GetProperty("errors").EnumerateArray();

        var expectedMessage = ResourceMessageException.ResourceManager.GetString("BIRTH_DATE_EMPTY", new CultureInfo(culture));

        errors.Should().HaveCount(1).And.Contain(e => e.GetString()!.Equals(expectedMessage));
    }

    [Theory]
    [ClassData(typeof(CultureInlineDataTest))]
    public async Task Error_Birth_Date_Invalid(string culture)
    {
        var command = RegisterDonorCommandBuilder.Builder();
        command.BirthDate = new DateTime(2026, 7, 7);
        command.Address = new BloodDonationDb.Domain.ValueObjects.Address(
            "",
            "123",
            "",
            "",
            "41815050",
            "Brasil");

        var token = JwtTokenGeneratorBuilder.Builder().Generate(_userId);

        var response = await PostAsync(method: METHOD, command: command, token: token, culture: culture);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        var errors = responseData.RootElement.GetProperty("errors").EnumerateArray();

        var expectedMessage = ResourceMessageException.ResourceManager.GetString("BIRTH_DATE_INVALID", new CultureInfo(culture));

        errors.Should().HaveCount(1).And.Contain(e => e.GetString()!.Equals(expectedMessage));
    }

    [Theory]
    [ClassData(typeof(CultureInlineDataTest))]

    public async Task Error_Birth_Date_Not_Allowed(string culture)
    {
        var command = RegisterDonorCommandBuilder.Builder();
        command.BirthDate = new DateTime(DateTime.UtcNow.Year - 69, 1, 1);
        command.Address = new BloodDonationDb.Domain.ValueObjects.Address(
            "",
            "123",
            "",
            "",
            "41815050",
            "Brasil");

        var token = JwtTokenGeneratorBuilder.Builder().Generate(_userId);

        var response = await PostAsync(method: METHOD, command: command, token: token, culture: culture);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        var errors = responseData.RootElement.GetProperty("errors").EnumerateArray();

        var expectedMessage = ResourceMessageException.ResourceManager.GetString("BIRTH_DATE_NOT_ALLOWED", new CultureInfo(culture));

        errors.Should().HaveCount(1).And.Contain(e => e.GetString()!.Equals(expectedMessage));
    }

    [Theory]
    [ClassData(typeof(CultureInlineDataTest))]
    public async Task Error_Gender_Not_Supported(string culture)
    {
        var command = RegisterDonorCommandBuilder.Builder();
        command.Gender = (BloodDonationDb.Domain.Enums.Gender)100;
        command.Address = new BloodDonationDb.Domain.ValueObjects.Address(
            "",
            "123",
            "",
            "",
            "41815050",
            "Brasil");

        var token = JwtTokenGeneratorBuilder.Builder().Generate(_userId);

        var response = await PostAsync(method: METHOD, command: command, token: token, culture: culture);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        var errors = responseData.RootElement.GetProperty("errors").EnumerateArray();

        var expectedMessage = ResourceMessageException.ResourceManager.GetString("GENDER_NOT_SUPPORTED", new CultureInfo(culture));

        errors.Should().HaveCount(1).And.Contain(e => e.GetString()!.Equals(expectedMessage));
    }

    [Theory]
    [ClassData(typeof(CultureInlineDataTest))]
    [InlineData("", 49)]
    [InlineData("", 141)]
    public async Task Error_Weight_Not_Allowed(string culture, int weight = 43)
    {
        var command = RegisterDonorCommandBuilder.Builder();

        command.Weight = weight;
        command.Address = new BloodDonationDb.Domain.ValueObjects.Address(
            "",
            "123",
            "",
            "",
            "41815050",
            "Brasil");

        var token = JwtTokenGeneratorBuilder.Builder().Generate(_userId);

        var response = await PostAsync(method: METHOD, command: command, token: token, culture: culture);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        var errors = responseData.RootElement.GetProperty("errors").EnumerateArray();

        var expectedMessage = ResourceMessageException.ResourceManager.GetString("WEIGHT_NOT_ALLOWED", new CultureInfo(culture));

        errors.Should().HaveCount(1).And.Contain(e => e.GetString()!.Equals(expectedMessage));
    }

    [Theory]
    [ClassData(typeof(CultureInlineDataTest))]
    public async Task Error_Blood_Type_Not_Supported(string culture)
    {
        var command = RegisterDonorCommandBuilder.Builder();
        command.BloodType = (BloodDonationDb.Domain.Enums.BloodType)100;
        command.Address = new BloodDonationDb.Domain.ValueObjects.Address(
            "",
            "123",
            "",
            "",
            "41815050",
            "Brasil");

        var token = JwtTokenGeneratorBuilder.Builder().Generate(_userId);

        var response = await PostAsync(method: METHOD, command: command, token: token, culture: culture);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        var errors = responseData.RootElement.GetProperty("errors").EnumerateArray();

        var expectedMessage = ResourceMessageException.ResourceManager.GetString("BLOOD_TYPE_NOT_SUPPOTED", new CultureInfo(culture));

        errors.Should().HaveCount(1).And.Contain(e => e.GetString()!.Equals(expectedMessage));
    }

    [Theory]
    [ClassData(typeof(CultureInlineDataTest))]
    public async Task Error_Rh_Factor_Not_Supported(string culture)
    {
        var command = RegisterDonorCommandBuilder.Builder();
        command.RhFactor = (BloodDonationDb.Domain.Enums.RhFactor)100;
        command.Address = new BloodDonationDb.Domain.ValueObjects.Address(
            "",
            "123",
            "",
            "",
            "41815050",
            "Brasil");

        var token = JwtTokenGeneratorBuilder.Builder().Generate(_userId);

        var response = await PostAsync(method: METHOD, command: command, token: token, culture: culture);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        var errors = responseData.RootElement.GetProperty("errors").EnumerateArray();

        var expectedMessage = ResourceMessageException.ResourceManager.GetString("RH_FACTOR_NOT_SUPPORTED", new CultureInfo(culture));

        errors.Should().HaveCount(1).And.Contain(e => e.GetString()!.Equals(expectedMessage));
    }
}
