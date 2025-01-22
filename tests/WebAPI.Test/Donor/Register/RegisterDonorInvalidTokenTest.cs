using CommomTestUtilities.Commands;
using CommomTestUtilities.Token;
using FluentAssertions;
using System.Net;

namespace WebAPI.Test.Donor.Register;
public class RegisterDonorInvalidTokenTest : BloodDonationDbClassFixture
{
    private const string METHOD = "donor";

    public RegisterDonorInvalidTokenTest(CustomWebApplicationFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task Error_Token_Invalid()
    {
        var command = RegisterDonorCommandBuilder.Builder();

        var response = await PostAsync(method: METHOD, command, token: "tokeninvalid");

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task Error_Without_Token()
    {
        var command = RegisterDonorCommandBuilder.Builder();

        var response = await PostAsync(method: METHOD, command, token: string.Empty);

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task Error_Token_With_User_NotFound()
    {
        var command = RegisterDonorCommandBuilder.Builder();

        var token = JwtTokenGeneratorBuilder.Builder().Generate(Guid.NewGuid());

        var response = await PostAsync(method: METHOD, command, token: token);

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
}
