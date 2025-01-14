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

namespace BloodDonationDb.Application.Commands.User.Register;

public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, ResultViewModel<ResponseRegisterUser>>
{
    private readonly IUserWriteOnlyRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordEncripter _passwordEncripter;
    private readonly IAccessTokenGenerator _accessTokenGenerator;
    private readonly IRefreshTokenGenerator _refreshTokenGenerator;
    private readonly ITokenRepository _tokenRepository;

    public RegisterUserHandler(IUserWriteOnlyRepository userRepository, 
        IUnitOfWork unitOfWork, 
        IPasswordEncripter passwordEncripter, 
        IAccessTokenGenerator accessTokenGenerator, 
        IRefreshTokenGenerator refreshTokenGenerator, 
        ITokenRepository tokenRepository)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _passwordEncripter = passwordEncripter;
        _accessTokenGenerator = accessTokenGenerator;
        _refreshTokenGenerator = refreshTokenGenerator;
        _tokenRepository = tokenRepository;
    }

    public async Task<ResultViewModel<ResponseRegisterUser>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {        
        var password = _passwordEncripter.Encript(request.Password!);

        var user = request.ToEntity(password);

        await _userRepository.AddUserAsync(user);

        await _unitOfWork.CommitAsync();

        var refreshToken = await CreateAndSaveRefreshToken(user);

        var responseRegisterUser = new ResponseRegisterUser
        {
            Name = user.Name,
            Token = new ResponseToken
            {
                AccessToken = _accessTokenGenerator.Generate(user.Id),
                RefreshToken = refreshToken
            }
        };

        return ResultViewModel<ResponseRegisterUser>.Success(responseRegisterUser);
    }

    private async Task<string> CreateAndSaveRefreshToken(Domain.Entities.User user)
    {
        var refreshToken = _refreshTokenGenerator.Generate();

        await _tokenRepository.SaveNewRefreshToken(new RefreshToken(refreshToken, user.Id));

        await _unitOfWork.CommitAsync();

        return refreshToken;       
    }
}
