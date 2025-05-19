using EduCenter.API.Base;
using EduCenter.API.Features.Users.RegisterUser;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using EduCenter.API.Features.Users.DTOs;
using System;
using EduCenter.API.Features.Users.GetUserById;
using EduCenter.API.Features.Users.AssignRole;

namespace EduCenter.API.Features.Users;
public class UserController : BaseApiController
{
    private readonly IMediator _mediator;
    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost("add")]
    [ProducesResponseType(typeof(UserViewModel), StatusCodes.Status201Created)]
    public async Task<IActionResult> Create([FromBody] RegisterUserCommand cmd, CancellationToken ct)
    {
        var user = await _mediator.Send(cmd, ct);
        return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
    }
    [HttpGet("get-by-id/{id}")]
    [ProducesResponseType(typeof(UserViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id, CancellationToken ct)
    {
        var query = new GetUserByIdQuery(id);
        UserViewModel user = await _mediator.Send(query, ct);
        return user == null ? NotFound() : Ok(user);
    }
    [HttpPost("assign-role")]
    public async Task<IActionResult> AssignRole(AssignRoleCommand request, CancellationToken ct)
    {
        await _mediator.Send(request, ct);
        return Ok();
    }
}
