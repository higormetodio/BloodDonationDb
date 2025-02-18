using BloodDonationDb.Application.Commands.Receiver;
using BloodDonationDb.Application.Models.Receiver;
using BloodDonationDb.Comunication.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BloodDonationDb.API.Controllers;

public class ReceiverController : MyBloodDonationDbController
{
    private readonly IMediator _mediator;

    public ReceiverController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(typeof(RegisterReceiverViewModel), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorViewModel), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RegisterReceiver([FromBody] RegisterReceiverCommand command)
    {
        var result = await _mediator.Send(command);

        return Created(string.Empty, result);
    }
}
