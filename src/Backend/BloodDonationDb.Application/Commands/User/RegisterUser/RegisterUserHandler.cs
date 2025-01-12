using BloodDonationDb.Application.Models;
using BloodDonationDb.Application.Models.User;
using BloodDonationDb.Domain.Repositories.User;
using BloodDonationDb.Domain.Security.Criptography;
using BloodDonationDb.Domain.SeedWorks;
using MediatR;

namespace BloodDonationDb.Application.Commands.User.RegisterUser;

public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, ResultViewModel<ResponseRegisterUser>>
{
    private readonly IUserWriteOnlyRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordEncripter _passwordEncripter;

    public RegisterUserHandler(IUserWriteOnlyRepository userRepository, IUnitOfWork unitOfWork, IPasswordEncripter passwordEncripter)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _passwordEncripter = passwordEncripter;
    }

    public async Task<ResultViewModel<ResponseRegisterUser>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {        
        var password = _passwordEncripter.Encript(request.Password!);

        var user = request.ToEntity(password);

        await _userRepository.AddUserAsync(user);

        await _unitOfWork.CommitAsync();

        var token = Guid.NewGuid().ToString();

        return ResultViewModel<ResponseRegisterUser>.Success(new ResponseRegisterUser(user.Name, token));
    }
}
