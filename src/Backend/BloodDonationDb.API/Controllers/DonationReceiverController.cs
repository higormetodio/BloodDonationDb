using BloodDonationDb.Comunication.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using BloodDonationDb.Application.Commands.DonationReceiver.Register;
using BloodDonationDb.Application.Models.DonationReceiver;

namespace BloodDonationDb.API.Controllers;

public class DonationReceiverController : MyBloodDonationDbController
{
    private readonly IMediator _mediator;
    public DonationReceiverController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(typeof(RegisterDonationReceiverViewModel), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorViewModel), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RegisterReceiver([FromBody] RegisterDonationReceiverCommand command)
    {
        var result = await _mediator.Send(command);

        return Created(string.Empty, result);
    }
}
