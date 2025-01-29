using BloodDonationDb.Application.Commands.Donor.Register;
using CommomTestUtilities.Commands;
using CommomTestUtilities.Token;
using FluentAssertions;
using System.Net;

namespace WebAPI.Test.DonationDonor;
public class RegisterDonationDonorInvalidToken : BloodDonationDbClassFixture
{
    private const string METHOD = "donationdonor";
    private readonly BloodDonationDb.Domain.Entities.Donor _donor;

    public RegisterDonationDonorInvalidToken(CustomWebApplicationFactory factory) : base(factory)
    {
        _donor = factory.GetDonor();
    }

    [Fact]
    public async Task Error_Token_Invalid()
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

        var response = await PostAsync(method: METHOD, command, token: "tokeninvalid");

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task Error_Without_Token()
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

        var response = await PostAsync(method: METHOD, command, token: string.Empty);

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task Error_Token_With_User_NotFound()
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

        var token = JwtTokenGeneratorBuilder.Builder().Generate(Guid.NewGuid());

        var response = await PostAsync(method: METHOD, command, token: token);

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
}
