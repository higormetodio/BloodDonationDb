using BloodDonationDb.Exceptions;
using CommomTestUtilities.Commands;
using CommomTestUtilities.Token;
using FluentAssertions;
using System.Globalization;
using System.Net;
using System.Text.Json;
using WebAPI.Test.InlineData;

namespace WebAPI.Test.User;
public class RegisterUserTest : BloodDonationDbClassFixture
{
    private const string method = "user";

    private readonly Guid _userId;

    public RegisterUserTest(CustomWebApplicationFactory factory) : base(factory)
    {
        _userId = factory.GetUserId();
    }

    [Fact]
    public async Task Success()
    {
        var command = RegisterUserCommandBuilder.Builder();

        var token = JwtTokenGeneratorBuilder.Builder().Generate(userIdentifier: _userId);

        var response = await PostAsync(method: method, command: command, token: token);

        response.StatusCode.Should().Be(HttpStatusCode.Created);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        responseData.RootElement.GetProperty("name").GetString().Should().NotBeNullOrWhiteSpace().And.Be(command.Name);
        responseData.RootElement.GetProperty("token").GetProperty("accessToken").GetString().Should().NotBeNullOrEmpty();
    }

    [Theory]
    [ClassData(typeof(CultureInlineDataTest))]
    public async Task Error_Name_Empty(string culture)
    {
        var command = RegisterUserCommandBuilder.Builder();
        command.Name = string.Empty;

        var token = JwtTokenGeneratorBuilder.Builder().Generate(userIdentifier: _userId);

        var response = await PostAsync(method: method, command: command, token: token, culture: culture);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        var errors = responseData.RootElement.GetProperty("errors").EnumerateArray();

        var expectedMessage = ResourceMessageException.ResourceManager.GetString("NAME_EMPTY", new CultureInfo(culture));

        errors.Should().ContainSingle().And.Contain(error => error.GetString()!.Equals(expectedMessage));
    }

    [Theory]
    [ClassData(typeof(CultureInlineDataTest))]
    public async Task Error_Email_Empty(string culture)
    {
        var command = RegisterUserCommandBuilder.Builder();
        command.Email = string.Empty;

        var token = JwtTokenGeneratorBuilder.Builder().Generate(userIdentifier: _userId);

        var response = await PostAsync(method: method, command: command, token: token, culture: culture);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        var errors = responseData.RootElement.GetProperty("errors").EnumerateArray();

        var expectedMessage = ResourceMessageException.ResourceManager.GetString("EMAIL_EMPTY", new CultureInfo(culture));

        errors.Should().ContainSingle().And.Contain(error => error.GetString()!.Equals(expectedMessage));
    }

    [Theory]
    [ClassData(typeof(CultureInlineDataTest))]
    public async Task Error_Email_Invalid(string culture)
    {
        var command = RegisterUserCommandBuilder.Builder();
        command.Email = "email.com.br";

        var token = JwtTokenGeneratorBuilder.Builder().Generate(userIdentifier: _userId);

        var response = await PostAsync(method: method, command: command, token: token, culture: culture);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        var errors = responseData.RootElement.GetProperty("errors").EnumerateArray();

        var expectedMessage = ResourceMessageException.ResourceManager.GetString("EMAIL_INVALID", new CultureInfo(culture));

        errors.Should().ContainSingle().And.Contain(error => error.GetString()!.Equals(expectedMessage));
    }

    [Theory]
    [ClassData(typeof(CultureInlineDataTest))]
    public async Task Error_Password_Empty(string culture)
    {
        var command = RegisterUserCommandBuilder.Builder();
        command.Password = string.Empty;

        var token = JwtTokenGeneratorBuilder.Builder().Generate(userIdentifier: _userId);

        var response = await PostAsync(method: method, command: command, token: token, culture: culture);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        var errors = responseData.RootElement.GetProperty("errors").EnumerateArray();

        var expectedMessage = ResourceMessageException.ResourceManager.GetString("PASSWORD_EMPTY", new CultureInfo(culture));

        errors.Should().ContainSingle().And.Contain(error => error.GetString()!.Equals(expectedMessage));
    }

    [Theory]
    [ClassData(typeof(CultureInlineDataTest))]
    public async Task Error_Password_Invalid(string culture)
    {
        var command = RegisterUserCommandBuilder.Builder();
        command.Password = "12345678910";

        var token = JwtTokenGeneratorBuilder.Builder().Generate(userIdentifier: _userId);

        var response = await PostAsync(method: method, command: command, token: token, culture: culture);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        var errors = responseData.RootElement.GetProperty("errors").EnumerateArray();

        var expectedMessage = ResourceMessageException.ResourceManager.GetString("PASSWORD_INVALID", new CultureInfo(culture));

        errors.Should().ContainSingle().And.Contain(error => error.GetString()!.Equals(expectedMessage));
    }

}