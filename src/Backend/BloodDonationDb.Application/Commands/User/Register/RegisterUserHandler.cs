using BloodDonationDb.Application.Models;
using BloodDonationDb.Application.Models.User;
using BloodDonationDb.Domain.Repositories.User;
using BloodDonationDb.Domain.Security.Criptography;
using BloodDonationDb.Domain.Security.Tokens;
using BloodDonationDb.Domain.SeedWorks;
using MediatR;

namespace BloodDonationDb.Application.Commands.User.Register;

public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, ResultViewModel<ResponseRegisterUser>>
{
    private readonly IUserWriteOnlyRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordEncripter _passwordEncripter;
    private readonly IAccessTokenGenerator _accessTokenGenerator;

    public RegisterUserHandler(IUserWriteOnlyRepository userRepository, IUnitOfWork unitOfWork, IPasswordEncripter passwordEncripter, IAccessTokenGenerator accessTokenGenerator)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _passwordEncripter = passwordEncripter;
        _accessTokenGenerator = accessTokenGenerator;
    }

    public async Task<ResultViewModel<ResponseRegisterUser>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {        
        var password = _passwordEncripter.Encript(request.Password!);

        var user = request.ToEntity(password);

        await _userRepository.AddUserAsync(user);

        await _unitOfWork.CommitAsync();

        var responseRegisterUser = new ResponseRegisterUser
        {
            Name = user.Name,
            Token = _accessTokenGenerator.Generate(user.Id)
        };

        return ResultViewModel<ResponseRegisterUser>.Success(responseRegisterUser);
    }
}
