using BloodDonationDb.Application.Commands.Login;
using BloodDonationDb.Exceptions;
using CommomTestUtilities.Commands;
using FluentAssertions;
using System.Configuration;
using System.Globalization;
using System.Net;
using System.Text.Json;
using WebAPI.Test.InlineData;

namespace WebAPI.Test.Login;
public class LoginUserTest : BloodDonationDbClassFixture
{
    private readonly string method = "login";

    private readonly string _email;
    private readonly string _password;
    private readonly string _name;

    public LoginUserTest(CustomWebApplicationFactory factory) : base(factory)
    {
        _email = factory.GetEmail();
        _password = factory.GetPassword();
        _name = factory.GetName();
    }

    [Fact]
    public async Task Login_User_Success()
    {
        var command = new LoginUserCommand
        {
            Email = _email,
            Password = _password,
        };

        var response = await PostAsync(method: method, command: command);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        responseData.RootElement.GetProperty("name").GetString().Should().NotBeNullOrWhiteSpace().And.Be(_name);
        responseData.RootElement.GetProperty("token").GetProperty("accessToken").GetString().Should().NotBeNullOrEmpty();
    }

    [Theory]
    [ClassData(typeof(CultureInlineDataTest))]
    public async Task Error_Login_Invalid(string culture)
    {
        var command = LoginUserCommandBuilder.Builder();

        var response = await PostAsync(method: method, command: command, culture: culture);

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        var errors = responseData.RootElement.GetProperty("errors").EnumerateArray();

        var expectedMessage = ResourceMessageException.ResourceManager.GetString("EMAIL_OR_PASSWORD_INVALID", new CultureInfo(culture));

        errors.Should().ContainSingle().And.Contain(error => error.GetString()!.Equals(expectedMessage));
    }

}
