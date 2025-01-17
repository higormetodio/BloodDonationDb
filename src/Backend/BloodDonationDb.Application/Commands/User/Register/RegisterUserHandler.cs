using BloodDonationDb.Application.Models;
using BloodDonationDb.Application.Models.User;
using BloodDonationDb.Domain.Repositories.Token;
using BloodDonationDb.Domain.Repositories.User;
using BloodDonationDb.Domain.Security.Criptography;
using BloodDonationDb.Domain.Security.Tokens;
using BloodDonationDb.Domain.SeedWorks;
using BloodDonationDb.Domain.Entities;
using MediatR;
using BloodDonationDb.Application.Models.Token;
using BloodDonationDb.Exceptions;
using BloodDonationDb.Exceptions.ExceptionsBase;

namespace BloodDonationDb.Application.Commands.User.Register;

public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, ResultViewModel<RegisterUserViewModel>>
{
    private readonly IUserWriteOnlyRepository _userWriteOnlyRepository;
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordEncripter _passwordEncripter;
    private readonly IAccessTokenGenerator _accessTokenGenerator;
    private readonly IRefreshTokenGenerator _refreshTokenGenerator;
    private readonly ITokenRepository _tokenRepository;

    public RegisterUserHandler(IUserWriteOnlyRepository userWriteOnlyRepository,
        IUserReadOnlyRepository userReadOnlyRepository,
        IUnitOfWork unitOfWork,
        IPasswordEncripter passwordEncripter,
        IAccessTokenGenerator accessTokenGenerator,
        IRefreshTokenGenerator refreshTokenGenerator,
        ITokenRepository tokenRepository)
    {
        _userWriteOnlyRepository = userWriteOnlyRepository;
        _userReadOnlyRepository = userReadOnlyRepository;
        _unitOfWork = unitOfWork;
        _passwordEncripter = passwordEncripter;
        _accessTokenGenerator = accessTokenGenerator;
        _refreshTokenGenerator = refreshTokenGenerator;
        _tokenRepository = tokenRepository;
        
    }

    public async Task<ResultViewModel<RegisterUserViewModel>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        await Validate(request);

        var password = _passwordEncripter.Encript(request.Password!);

        var user = request.ToEntity(password);

        await _userWriteOnlyRepository.AddUserAsync(user);

        await _unitOfWork.CommitAsync();

        var refreshToken = await CreateAndSaveRefreshToken(user);

        var responseRegisterUser = new RegisterUserViewModel
        {
            Name = user.Name,
            Token = new TokenViewModel
            {
                AccessToken = _accessTokenGenerator.Generate(user.Id),
                RefreshToken = refreshToken
            }
        };

        return ResultViewModel<RegisterUserViewModel>.Success(responseRegisterUser);
    }

    private async Task<string> CreateAndSaveRefreshToken(Domain.Entities.User user)
    {
        var refreshToken = _refreshTokenGenerator.Generate();

        await _tokenRepository.SaveNewRefreshToken(new RefreshToken(refreshToken, user.Id));

        await _unitOfWork.CommitAsync();

        return refreshToken;       
    }

    private async Task Validate(RegisterUserCommand command)
    {
        var validator = new RegisterUserValidator();

        var result = await validator.ValidateAsync(command);

        var emailExist = await _userReadOnlyRepository.ExistsActiveUserWithEmail(command.Email!);

        if (emailExist)
        {
            result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, ResourceMessageException.EMAIL_ALREADY_REGISTER));
        }

        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }

    }
}
