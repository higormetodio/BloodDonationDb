using BloodDonationDb.Exceptions;
using CommomTestUtilities.Commands;
using CommomTestUtilities.Queries;
using CommomTestUtilities.Token;
using FluentAssertions;
using System.Globalization;
using System.Net;
using System.Text.Json;
using WebAPI.Test.InlineData;

namespace WebAPI.Test.Donor.GetByEmail;
public class GetDonorByEmailTest : BloodDonationDbClassFixture
{
    public const string METHOD = "donor";
    public readonly Guid _uderId;
    public readonly BloodDonationDb.Domain.Entities.Donor _donor;
    public GetDonorByEmailTest(CustomWebApplicationFactory factory) : base(factory)
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
        responseData.RootElement.GetProperty("birthDate").GetDateTime().Should().Be(_donor.BirthDate);
        responseData.RootElement.GetProperty("gender").GetInt32().Should().Be((int)_donor.Gender);
        responseData.RootElement.GetProperty("weight").GetInt32().Should().Be(_donor.Weight);
        responseData.RootElement.GetProperty("isDonor").GetBoolean().Should().Be(_donor.IsDonor);
        responseData.RootElement.GetProperty("lastDonation").GetString().Should().BeNullOrEmpty(_donor.LastDonation.ToString());
        responseData.RootElement.GetProperty("nextDonation").GetString().Should().BeNullOrEmpty(_donor.NextDonation.ToString());
        responseData.RootElement.GetProperty("bloodType").GetString().Should().Be(_donor.BloodType.ToString());
        responseData.RootElement.GetProperty("rhFactor").GetString().Should().Be(_donor.RhFactor.ToString());
        responseData.RootElement.GetProperty("address").GetProperty("street").GetString().Should().Be(_donor.Address?.Street);
        responseData.RootElement.GetProperty("active").GetBoolean().Should().Be(_donor.Active);
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
