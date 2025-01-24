using Azure;
using BloodDonationDb.Application.Commands.DonorDonation.Register;
using BloodDonationDb.Application.Models.DonorDonation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BloodDonationDb.API.Controllers;

public class DonationDonorController : MyBloodDonationDbController
{
    private readonly IMediator _mediator;

    public DonationDonorController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(typeof(RegisterDonationDonorViewModel), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseError), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RegisterDonorDonation([FromBody]RegisterDonationDonorCommand command)
    {
        var result = await _mediator.Send(command);

        return Created(string.Empty, result);
    }
}
