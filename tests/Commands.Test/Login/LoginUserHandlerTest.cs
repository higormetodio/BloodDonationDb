using BloodDonationDb.Application.Commands.Login;
using BloodDonationDb.Application.Models.User;
using BloodDonationDb.Domain.Entities;
using BloodDonationDb.Exceptions;
using BloodDonationDb.Exceptions.ExceptionsBase;
using CommomTestUtilities.Commands;
using CommomTestUtilities.Criptography;
using CommomTestUtilities.Domain.Entities;
using CommomTestUtilities.Repositories.User;
using CommomTestUtilities.Token;
using FluentAssertions;

namespace Commands.Test.Login;
public class LoginUserHandlerTest
{
    [Fact]
    public async Task Login_User_Success()
    {
        var (user, password) = UserBuilder.Builder();

        var handler = CreateHandler(user);

        var resultViewModel = await handler.Handle(request: new LoginUserCommand
        {
            Email = user.Email,
            Password = password
        }, cancellationToken: CancellationToken.None);

        var result = resultViewModel.Data;

        result.Should().NotBeNull();
        result!.Token.Should().NotBeNull();
        result.Name.Should().NotBeNull().And.Be(user.Name);
        result.Token!.AccessToken.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task Error_Invalid_User()
    {
        var command = LoginUserCommandBuilder.Builder();

        var handler = CreateHandler();

        Func<Task> act = async () => await handler.Handle(request: command, cancellationToken: CancellationToken.None);

        await act.Should().ThrowAsync<InvalidLoginException>()
            .Where(e => e.Message.Equals(ResourceMessageException.EMAIL_OR_PASSWORD_INVALID));
    }

    public LoginUserHandler CreateHandler(BloodDonationDb.Domain.Entities.User? user = null)
    {
        var userReadOnlyRepository = new UserReadOnlyRepositoryBuilder();
        var passwordEncripter = PasswordEncripterBuilder.Builder();
        var accessTokenGenerator = JwtTokenGeneratorBuilder.Builder();

        if (user is not null)
        {
            userReadOnlyRepository.GetByEmail(user);
        }

        return new LoginUserHandler(userReadOnlyRepository.Builder(), passwordEncripter, accessTokenGenerator);

        
    }
}
