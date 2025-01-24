using BloodDonationDb.API.Attributes;
using BloodDonationDb.Application.Commands.Donor.Register;
using BloodDonationDb.Application.Models;
using BloodDonationDb.Application.Models.Donor;
using BloodDonationDb.Application.Queries.Donor.GetDonor;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BloodDonationDb.API.Controllers;

//[AuthenticatedUser]
public class DonorController : MyBloodDonationDbController
{
    private readonly IMediator _mediator;

    public DonorController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{email}")]
    [ProducesResponseType(typeof(DonorViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultViewModel), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetDonor([FromRoute]string email)
    {
        var query = new GetDonorByEmailQuery(email);

        var result = await _mediator.Send(query);

        return Ok(result.Data);
    }
    
    [HttpPost]
    [ProducesResponseType(typeof(RegisterDonorViewModel), StatusCodes.Status201Created)]
    public async Task<IActionResult> RegisterDonor([FromBody]RegisterDonorCommand command)
    {
        var result = await _mediator.Send(command);

        return Created(string.Empty, result.Data);
    }
}
