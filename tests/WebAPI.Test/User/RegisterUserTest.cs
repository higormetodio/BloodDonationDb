using CommomTestUtilities.Commands;
using FluentAssertions;
using System.Net;
using System.Text.Json;

namespace WebAPI.Test.User;
public class RegisterUserTest : BloodDonationDbClassFixture
{
    private readonly string method = "user";
    public RegisterUserTest(CustomWebApplicationFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task RegisterUser_Success()
    {
        var command = RegisterUserCommandBuilder.Builder();

        var response = await PostAsync(method: method, command: command);

        response.StatusCode.Should().Be(HttpStatusCode.Created);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        responseData.RootElement.GetProperty("name").GetString().Should().NotBeNullOrWhiteSpace().And.Be(command.Name);
        responseData.RootElement.GetProperty("token").GetProperty("accessToken").GetString().Should().NotBeNullOrEmpty();
    }
}