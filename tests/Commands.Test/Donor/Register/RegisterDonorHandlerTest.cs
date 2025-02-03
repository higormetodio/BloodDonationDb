using BloodDonationDb.Application.Commands.Donor.Register;
using BloodDonationDb.Application.DTOs;
using BloodDonationDb.Exceptions;
using BloodDonationDb.Exceptions.ExceptionsBase;
using CommomTestUtilities.Commands;
using CommomTestUtilities.Repositories;
using CommomTestUtilities.Repositories.Donor;
using CommomTestUtilities.Services;
using FluentAssertions;

namespace Commands.Test.Donor.Register;
public class RegisterDonorHandlerTest
{
    [Fact]
    public async Task Success()
    {
        var enderecoDto = new EnderecoDTO
        {
            CEP = "41815050",
            Logradouro = "Rua Padre Manuel Barbosa",
            Localidade = "Salvador",
            Uf = "BA"
        };

        var command = RegisterDonorCommandBuilder.Builder();

        var handler = CreateHandler(enderecoDto);

        var registerDonorViewModel = await handler.Handle(request: command, cancellationToken: CancellationToken.None);

        var result = registerDonorViewModel;

        result.Should().NotBeNull();
        result!.Name.Should().NotBeEmpty();
    }

    [Fact]
    public async Task Error_CEP_Not_Found()
    {
        EnderecoDTO? enderecoDto = null;

        var command = RegisterDonorCommandBuilder.Builder();

        var handler = CreateHandler(enderecoDto!);

        Func<Task> act = async () => await handler.Handle(request: command, cancellationToken: CancellationToken.None);

        (await act.Should().ThrowAsync<NotFoundException>())
            .Where(e => e.GetErrorMessages().Count == 1 && e.GetErrorMessages().Contains(ResourceMessageException.CEP_NOT_FOUND));
    }

    [Fact]
    public async Task Error_Email_Already_Registred()
    {
        var enderecoDto = new EnderecoDTO
        {
            CEP = "41815050",
            Logradouro = "Rua Padre Manuel Barbosa",
            Localidade = "Salvador",
            Uf = "BA"
        };

        var command = RegisterDonorCommandBuilder.Builder();

        var handler = CreateHandler(enderecoDto, command.Email!);

        Func<Task> act = async () => await handler.Handle(request: command, cancellationToken: CancellationToken.None);

        (await act.Should().ThrowAsync<ErrorOnValidationException>())
            .Where(e => e.GetErrorMessages().Count == 1 && e.GetErrorMessages().Contains(ResourceMessageException.EMAIL_ALREADY_REGISTER));
    }

    private static RegisterDonorHandler CreateHandler(EnderecoDTO dto, string? email = null)
    {
        var donorWriteOnlyRepository = DonorWriteOnlyRepositoryBuilder.Builder();
        var donorReadOnlyRepository = new DonorReadOnlyRepositoryBuilder();
        var unityOfWork = UnitOfWorkBuilder.Builder();
        var getCepService = GetCepServiceBuilder.Builder(dto);

        if (!string.IsNullOrEmpty(email))
        {
            donorReadOnlyRepository.ExistActiveDonorWithEmail(email);
        }

        return new RegisterDonorHandler(donorWriteOnlyRepository, donorReadOnlyRepository.Builder(), unityOfWork, getCepService);


    }
}
