using BloodDonationDb.Application.Models;
using BloodDonationDb.Application.Models.Token;
using BloodDonationDb.Application.Models.User;
using BloodDonationDb.Domain.Repositories.User;
using BloodDonationDb.Domain.Security.Criptography;
using BloodDonationDb.Domain.Security.Tokens;
using BloodDonationDb.Exceptions.ExceptionsBase;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonationDb.Application.Commands.Login;
public class LoginUserHandler : IRequestHandler<LoginUserCommand, ResultViewModel<RegisterUserViewModel>>
{
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;
    private readonly IPasswordEncripter _passwordEncripter;
    private readonly IAccessTokenGenerator _accessTokenGenerator;

    public LoginUserHandler(IUserReadOnlyRepository userReadOnlyRepository, IPasswordEncripter passwordEncripter, IAccessTokenGenerator accessTokenGenerator)
    {
        _userReadOnlyRepository = userReadOnlyRepository;
        _passwordEncripter = passwordEncripter;
        _accessTokenGenerator = accessTokenGenerator;
    }

    public async Task<ResultViewModel<RegisterUserViewModel>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {        
        var user = await _userReadOnlyRepository.GetByEmailAsync(request.Email!);

        if (user is null || !_passwordEncripter.IsValid(request.Password!, user.Password))
        {
            throw new InvalidLoginException();
        }


        var responseRegisterUser = new RegisterUserViewModel
        {
            Name = user.Name,
            Token = new TokenViewModel
            {
                AccessToken = _accessTokenGenerator.Generate(user.Id),
            }
        };

        return ResultViewModel<RegisterUserViewModel>.Success(responseRegisterUser);
    }
}
