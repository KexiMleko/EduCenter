using EduCenter.API.Base;
using EduCenter.API.Features.Users.RegisterUser;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using EduCenter.API.Features.Users.DTOs;
using System;
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
    public async Task<IActionResult> Create(RegisterUserCommand cmd, CancellationToken ct)
    {
        var userView = await _mediator.Send(cmd, ct);
        Console.WriteLine($"USER VIEW id: {userView.Username}");
        return Ok(userView);
    }
    [HttpGet("get-by-id")]
    public async Task<IActionResult> GetById()
    {
        return Ok();
    }
}
