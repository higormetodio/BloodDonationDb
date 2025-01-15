using BloodDonationDb.Application.Models;
using BloodDonationDb.Application.Models.User;
using MediatR;

namespace BloodDonationDb.Application.Commands.Login;
public class LoginUserCommand : IRequest<ResultViewModel<ResponseRegisterUser>>
{
    public string? Email { get; set; }
    public string? Password { get; set; }
}
