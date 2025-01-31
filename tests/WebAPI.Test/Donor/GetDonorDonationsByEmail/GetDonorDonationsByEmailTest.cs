using BloodDonationDb.Exceptions;
using CommomTestUtilities.Token;
using FluentAssertions;
using System.Globalization;
using System.Net;
using System.Text.Json;
using WebAPI.Test.InlineData;

namespace WebAPI.Test.Donor.GetDonorDonationsByEmail;
public class GetDonorDonationsByEmailTest : BloodDonationDbClassFixture
{
    public const string METHOD = "donor/donations";
    public readonly Guid _uderId;
    public readonly BloodDonationDb.Domain.Entities.Donor _donor;

    public GetDonorDonationsByEmailTest(CustomWebApplicationFactory factory) : base(factory)
    {
        _uderId = factory.GetUserId();
        _donor = factory.GetDonor();
    }

    [Fact]
    public async Task Success()
    {
        var token = JwtTokenGeneratorBuilder.Builder().Generate(_uderId);

        var response = await GetAsync(method: $"{METHOD}/{_donor.Email}", token: token);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        responseData.RootElement.GetProperty("donorId").GetString().Should().NotBeNullOrWhiteSpace();
        responseData.RootElement.GetProperty("name").GetString().Should().Be(_donor.Name);
        responseData.RootElement.GetProperty("email").GetString().Should().Be(_donor.Email);
        responseData.RootElement.GetProperty("bloodType").GetString().Should().Be(_donor.BloodType.ToString());
        responseData.RootElement.GetProperty("rhFactor").GetString().Should().Be(_donor.RhFactor.ToString());
        responseData.RootElement.GetProperty("isDonor").GetBoolean().Should().Be(_donor.IsDonor);
    }

    [Theory]
    [ClassData(typeof(CultureInlineDataTest))]
    public async Task Error_Donor_Not_Found(string culture)
    {
        var token = JwtTokenGeneratorBuilder.Builder().Generate(_uderId);

        var response = await GetAsync(method: $"{METHOD}/fake_email@emailtest.com", token: token, culture: culture);

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        var errors = responseData.RootElement.GetProperty("errors").EnumerateArray();

        var expectedMessage = ResourceMessageException.ResourceManager.GetString("DONOR_NOT_FOUND", new CultureInfo(culture));

        errors.Should().HaveCount(1).And.Contain(e => e.GetString()!.Equals(expectedMessage));
    }
}
