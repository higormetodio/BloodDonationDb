using CommomTestUtilities.Token;
using FluentAssertions;
using System.Net;

namespace WebAPI.Test.Donor.GetDonorByEmail;
public class GetDonorByEmailInvalidTokenTest : BloodDonationDbClassFixture
{
    private const string METHOD = "donor";
    private readonly BloodDonationDb.Domain.Entities.Donor _donor;

    public GetDonorByEmailInvalidTokenTest(CustomWebApplicationFactory factory) : base(factory)
    {
        _donor = factory.GetDonor();
    }
    [Fact]
    public async Task Error_Token_Invalid()
    {
        var response = await GetAsync(method: $"{METHOD}/{_donor.Email}", token: "tokeninvalid");

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task Error_Without_Token()
    {
        var response = await GetAsync(method: $"{METHOD}/{_donor.Email}", token: string.Empty);

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task Error_Token_With_User_NotFound()
    {
        var token = JwtTokenGeneratorBuilder.Builder().Generate(Guid.NewGuid());

        var response = await GetAsync(method: $"{METHOD}/{_donor.Email}", token: token);

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
}
