using BloodDonationDb.Application.Commands.User.Register;
using BloodDonationDb.Exceptions;
using BloodDonationDb.Exceptions.ExceptionsBase;
using CommomTestUtilities.Commands;
using CommomTestUtilities.Criptography;
using CommomTestUtilities.Repositories;
using CommomTestUtilities.Repositories.Token;
using CommomTestUtilities.Repositories.User;
using CommomTestUtilities.Token;
using FluentAssertions;

namespace Commands.Test.User.Register;
public class RegisterUserHandlerTest
{
    [Fact]
    public async Task RegisterUserHandler_Success()
    {
        var command = RegisterUserCommandBuilder.Builder();
        
        var handler = CreateHandler();

        var resultViewModel = await handler.Handle(request: command, cancellationToken: CancellationToken.None);

        var result = resultViewModel.Data;

        result.Should().NotBeNull();
        result!.Name.Should().Be(command.Name);
        result.Token!.AccessToken.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task Error_Email_Already_Registered()
    {
        var command = RegisterUserCommandBuilder.Builder();

        var handler = CreateHandler(command.Email!);

        Func<Task> act = async () => await handler.Handle(request: command, CancellationToken.None);

        (await act.Should().ThrowAsync<ErrorOnValidationException>())
            .Where(e => e.GetErrorMessages().Count == 1 && e.GetErrorMessages().Contains(ResourceMessageException.EMAIL_ALREADY_REGISTER));
    }

    [Fact]
    public async Task Error_Name_Empty()
    {
        var command = RegisterUserCommandBuilder.Builder();
        command.Name = string.Empty;

        var handler = CreateHandler();

        Func<Task> act = async () => await handler.Handle(request: command, CancellationToken.None);

        (await act.Should().ThrowAsync<ErrorOnValidationException>())
            .Where(e => e.GetErrorMessages().Count == 1 && e.GetErrorMessages().Contains(ResourceMessageException.NAME_EMPTY));
    }

    [Fact]
    public async Task Error_Email_Empty()
    {
        var command = RegisterUserCommandBuilder.Builder();
        command.Email = string.Empty;

        var handler = CreateHandler();

        Func<Task> act = async () => await handler.Handle(request: command, CancellationToken.None);

        (await act.Should().ThrowAsync<ErrorOnValidationException>())
            .Where(e => e.GetErrorMessages().Count == 1 && e.GetErrorMessages().Contains(ResourceMessageException.EMAIL_EMPTY));
    }

    [Fact]
    public async Task Error_Email_Invalid()
    {
        var command = RegisterUserCommandBuilder.Builder();
        command.Email = "email.com.br";

        var handler = CreateHandler();

        Func<Task> act = async () => await handler.Handle(request: command, CancellationToken.None);

        (await act.Should().ThrowAsync<ErrorOnValidationException>())
            .Where(e => e.GetErrorMessages().Count == 1 && e.GetErrorMessages().Contains(ResourceMessageException.EMAIL_INVALID));
    }

    [Fact]
    public async Task Error_Password_Empty()
    {
        var command = RegisterUserCommandBuilder.Builder();
        command.Password = string.Empty;

        var handler = CreateHandler();

        Func<Task> act = async () => await handler.Handle(request: command, CancellationToken.None);

        (await act.Should().ThrowAsync<ErrorOnValidationException>())
            .Where(e => e.GetErrorMessages().Count == 1 && e.GetErrorMessages().Contains(ResourceMessageException.PASSWORD_EMPTY));
    }

    [Fact]
    public async Task Error_Password_Invalid()
    {
        var command = RegisterUserCommandBuilder.Builder();
        command.Password = "12345678910";

        var handler = CreateHandler();

        Func<Task> act = async () => await handler.Handle(request: command, CancellationToken.None);

        (await act.Should().ThrowAsync<ErrorOnValidationException>())
            .Where(e => e.GetErrorMessages().Count == 1 && e.GetErrorMessages().Contains(ResourceMessageException.PASSWORD_INVALID));
    }

    private static RegisterUserHandler CreateHandler(string? email = null)
    {                
        var userWriteOnlyRepository = UserWriteOnlyRepositoryBuilder.Builder();
        var userReadOnlyRepository = new UserReadOnlyRepositoryBuilder();
        var unitOfWork = UnitOfWorkBuilder.Builder();
        var passwordEncripter = PasswordEncripterBuilder.Builder();
        var accessTokenGenerator = JwtTokenGeneratorBuilder.Builder();
        var refreshTokenGenerator = RefreshTokenGeneratorBuilder.Build();
        var tokenRepository = new TokenRepositoryBuilder().Build();

        if (!string.IsNullOrEmpty(email))
        {
            userReadOnlyRepository.ExistActiveUserWithEmail(email!);
        }

        return new RegisterUserHandler(userWriteOnlyRepository, userReadOnlyRepository.Builder(), unitOfWork, passwordEncripter, accessTokenGenerator, refreshTokenGenerator, tokenRepository);
    }
}
