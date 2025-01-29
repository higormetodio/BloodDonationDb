using BloodDonationDb.Application.Commands.DonationDonor.Register;
using BloodDonationDb.Domain.Enums;
using BloodDonationDb.Domain.ValueObjects;
using BloodDonationDb.Exceptions;
using BloodDonationDb.Exceptions.ExceptionsBase;
using CommomTestUtilities.Commands;
using CommomTestUtilities.Domain.Entities;
using CommomTestUtilities.Repositories;
using CommomTestUtilities.Repositories.BloodStock;
using CommomTestUtilities.Repositories.DonationDonor;
using CommomTestUtilities.Repositories.Donor;
using FluentAssertions;

namespace Commands.Test.DonationDonor;
public class RegisterDonationDonorHandlerTest
{
    [Fact]
    public async Task Success()
    {
        var registerDonorCommand = RegisterDonorCommandBuilder.Builder();

        var command = RegisterDonationDonorCommandBuilder.Builder(registerDonorCommand);

        var donor = registerDonorCommand.ToEntity();

        var bloodStock = BloodStockBuilder.Builder(donor);

        var handler = CreateHandler(donor, bloodStock);

        var result = await handler.Handle(command, CancellationToken.None);

        result.Should().NotBeNull();
        result.Name.Should().NotBeEmpty();
        result.Blood.Should().NotBeEmpty();
        result.DonatioDate.Should().Be(command.DonationDate);
        result.Quantity.Should().Be(command.Quantity);
    }

    [Fact]
    public async Task Error_Donor_Not_Found()
    {
        var registerDonorCommand = RegisterDonorCommandBuilder.Builder();
        registerDonorCommand.Email = "changeemail@email.com";

        var command = RegisterDonationDonorCommandBuilder.Builder(registerDonorCommand);

        var handler = CreateHandler();

        Func<Task> act = async () => await handler.Handle(command, CancellationToken.None);

        await act.Should().ThrowAsync<NotFoundException>()
            .Where(e => e.GetErrorMessages().Count == 1 && e.GetErrorMessages().Contains(ResourceMessageException.DONOR_NOT_FOUND));
    }

    [Fact]
    public async Task Error_Donor_Cannot_Donation()
    {
        var registerDonorCommand = RegisterDonorCommandBuilder.Builder();
        registerDonorCommand.BirthDate = DateTime.UtcNow.AddDays(-14);

        var command = RegisterDonationDonorCommandBuilder.Builder(registerDonorCommand);

        var donor = registerDonorCommand.ToEntity();

        var bloodStock = BloodStockBuilder.Builder(donor);

        var handler = CreateHandler(donor, bloodStock);

        Func<Task> act = async () => await handler.Handle(command, CancellationToken.None);

        await act.Should().ThrowAsync<ErrorOnValidationException>()
            .Where(e => e.GetErrorMessages().Count == 1 && e.GetErrorMessages().Contains(ResourceMessageException.DONOR_CANNOT_DONATION));
    }

    [Fact]
    public async Task Error_Donation_Date_Not_AllowedGender()
    {
        var registerDonorCommand = RegisterDonorCommandBuilder.Builder();

        var command = RegisterDonationDonorCommandBuilder.Builder(registerDonorCommand);

        var donor = registerDonorCommand.ToEntity();
        
        if (donor.Gender == Gender.Male)
        {
            donor.UpdateLastDonation(command.DonationDate.AddDays(-(BloodDonationRuleConstants.DAYS_NEXT_DONATION_MAN - 5)));
        }
        else
        {
            donor.UpdateLastDonation(command.DonationDate.AddDays(-(BloodDonationRuleConstants.DAYS_NEXT_DONATION_WOMAN - 5)));
        }

        var bloodStock = BloodStockBuilder.Builder(donor);

        var handler = CreateHandler(donor, bloodStock);

        Func<Task> act = async () => await handler.Handle(command, CancellationToken.None);

        await act.Should().ThrowAsync<ErrorOnValidationException>()
            .Where(e => e.GetErrorMessages().Count == 1 && e.GetErrorMessages()
            .Contains($"{ResourceMessageException.DONATION_NOT_ALLOWED_NEXT_DONATION_DATE} {donor.NextDonation!.Value.Date.ToShortDateString()}"));
    }

    public RegisterDonationDonorHandler CreateHandler(BloodDonationDb.Domain.Entities.Donor? donor = null,
        BloodDonationDb.Domain.Entities.BloodStock? bloodStock = null)
    {
        var donationDornorWriteOnlyRepository = DonationDonorWriteOnlyRepositoryBuilder.Builder();
        var donorReadOnlyRepository = new DonorReadOnlyRepositoryBuilder();
        var donorUpdateOnlyRepository = DonorUpdateOnlyRepositoryBuilder.Builder();
        var bloodStockReadOnlyRepository = new BloodStockReadOnlyRepositoryBuilder();
        var bloodStockUpdateOnlyRepository = BloodStockUpdateOnlyRepositoryBuilder.Builder();
        var unitOfWork = UnitOfWorkBuilder.Builder();

        if (donor is not null)
        {
            donorReadOnlyRepository.GetDonorByEmail(donor);
            bloodStockReadOnlyRepository.GetBloodStock(bloodStock!);
        }

        return new RegisterDonationDonorHandler(
            donationDornorWriteOnlyRepository,
            donorReadOnlyRepository.Builder(),
            donorUpdateOnlyRepository,
            bloodStockReadOnlyRepository.Builder(),
            bloodStockUpdateOnlyRepository,
            unitOfWork);
    }
}
