using BloodDonationDb.Application.Commands.Login;
using BloodDonationDb.Application.Models.User;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BloodDonationDb.API.Controllers;

public class LoginController : MyBloodDonationDbController
{
    private readonly IMediator _mediator;

    public LoginController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(typeof(RegisterUserViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(RegisterUserViewModel), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
    {
        var request = await _mediator.Send(command);

        return Ok(request);
    }
}
