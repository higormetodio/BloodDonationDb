using CommomTestUtilities.Commands;
using CommomTestUtilities.Token;
using FluentAssertions;
using System.Net;

namespace WebAPI.Test.User;
public class RegisterUserInvalidTokenTest : BloodDonationDbClassFixture
{
    private const string METHOD = "user";

    public RegisterUserInvalidTokenTest(CustomWebApplicationFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task Error_Token_Invalid()
    {
        var command = RegisterUserCommandBuilder.Builder();

        var response = await PostAsync(method: METHOD, command, token: "tokeninvalid");

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task Error_Without_Token()
    {
        var command = RegisterUserCommandBuilder.Builder();

        var response = await PostAsync(method: METHOD, command, token: string.Empty);

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task Error_Token_With_User_NotFound()
    {
        var command = RegisterUserCommandBuilder.Builder();

        var token = JwtTokenGeneratorBuilder.Builder().Generate(Guid.NewGuid());

        var response = await PostAsync(method: METHOD, command, token: token);

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }


}
