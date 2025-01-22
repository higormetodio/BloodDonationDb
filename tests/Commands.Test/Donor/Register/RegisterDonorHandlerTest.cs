using BloodDonationDb.Application.Commands.Donor.Register;
using BloodDonationDb.Domain.Enums;
using BloodDonationDb.Exceptions;
using BloodDonationDb.Exceptions.ExceptionsBase;
using CommomTestUtilities.Commands;
using CommomTestUtilities.Repositories;
using CommomTestUtilities.Repositories.Donor;
using FluentAssertions;

namespace Commands.Test.Donor.Register;
public class RegisterDonorHandlerTest
{
    [Fact]
    public async Task Success()
    {
        var command = RegisterDonorCommandBuilder.Builder();

        var handler = CreateHandler();

        var resultViewModel = await handler.Handle(request: command, cancellationToken: CancellationToken.None);

        var result = resultViewModel.Data;

        result.Should().NotBeNull();
        result!.Name.Should().NotBeEmpty();
        result.Email.Should().NotBeEmpty();
        result.BloodType.Should().BeOneOf([BloodType.O, BloodType.B, BloodType.A, BloodType.AB]);
        result.RhFactor.Should().BeOneOf([RhFactor.Positive, RhFactor.Negative]);
    }

    [Fact]
    public async Task Error_Email_Already_Registred()
    {
        var command = RegisterDonorCommandBuilder.Builder();

        var handler = CreateHandler(command.Email!);

        Func<Task> act = async () => await handler.Handle(request: command, cancellationToken: CancellationToken.None);

        (await act.Should().ThrowAsync<ErrorOnValidationException>())
            .Where(e => e.GetErrorMessages().Count == 1 && e.GetErrorMessages().Contains(ResourceMessageException.EMAIL_ALREADY_REGISTER));
    }

    private static RegisterDonorHandler CreateHandler(string email = null)
    {
        var donorWriteOnlyRepository = DonorWriteOnlyRepositoryBuilder.Builder();
        var donorReadOnlyRepository = new DonorReadOnlyRepositoryBuilder();
        var unityOfWork = UnitOfWorkBuilder.Builder();

        if (!string.IsNullOrEmpty(email))
        {
            donorReadOnlyRepository.ExistActiveDonorWithEmail(email);
        }

        return new RegisterDonorHandler(donorWriteOnlyRepository, donorReadOnlyRepository.Builder(), unityOfWork);


    }
}
