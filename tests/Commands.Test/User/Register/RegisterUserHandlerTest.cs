using BloodDonationDb.Application.Commands.User.Register;
using CommomTestUtilities.Commands;
using CommomTestUtilities.Criptography;
using CommomTestUtilities.Repositories;
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
        result.Token.Should().NotBeNullOrEmpty();
    }

    private static RegisterUserHandler CreateHandler()
    {                
        var userWriteOnlyRepository = UserWriteOnlyRepositoryBuilder.Builder();
        var unitOfWork = UnitOfWorkBuilder.Builder();
        var passwordEncripter = PasswordEncripterBuilder.Builder();
        var accessTokenGenerator = JwtTokenGeneratorBuilder.Builder();

        return new RegisterUserHandler(userWriteOnlyRepository, unitOfWork, passwordEncripter, accessTokenGenerator);
    }
}
