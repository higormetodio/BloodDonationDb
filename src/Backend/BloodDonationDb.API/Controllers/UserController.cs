﻿using BloodDonationDb.API.Attributes;
using BloodDonationDb.Application.Commands.User.Register;
using BloodDonationDb.Application.Models.User;
using BloodDonationDb.Comunication.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BloodDonationDb.API.Controllers;

[AuthenticatedUser]
public class UserController : MyBloodDonationDbController
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(typeof(RegisterUserViewModel), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorViewModel), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
    {
        var result = await _mediator.Send(command);

        return Created(string.Empty, result);        
    }
}
