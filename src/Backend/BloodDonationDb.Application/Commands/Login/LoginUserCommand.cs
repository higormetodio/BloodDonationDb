using BloodDonationDb.Application.Models.User;
using MediatR;

namespace BloodDonationDb.Application.Commands.Login;
public class LoginUserCommand : IRequest<RegisterUserViewModel>
{
    public string? Email { get; set; }
    public string? Password { get; set; }
}
