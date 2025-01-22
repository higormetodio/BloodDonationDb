using BloodDonationDb.API.Attributes;
using BloodDonationDb.Application.Commands.Donor.Register;
using BloodDonationDb.Application.Models.Donor;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BloodDonationDb.API.Controllers;

[AuthenticatedUser]
public class DonorController : MyBloodDonationDbController
{
    private readonly IMediator _mediator;

    public DonorController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(typeof(RegisterDonorViewModel), StatusCodes.Status201Created)]
    public async Task<IActionResult> RegisterDonor([FromBody]RegisterDonorCommand command)
    {
        var result = await _mediator.Send(command);

        return Created(string.Empty, result.Data);
    }
}
