using EduCenter.API.Base;
using EduCenter.API.Features.Users.RegisterUser;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using EduCenter.API.Features.Users.DTOs;
using EduCenter.API.Features.Users.GetUserById;
using EduCenter.API.Features.Users.AssignRole;
using Microsoft.AspNetCore.Authorization;
using EduCenter.API.Features.Users.GetAllUsers;
using EduCenter.API.Shared.Filters;
using EduCenter.API.Features.Users.GetUsersPaged;
using EduCenter.API.Features.Users.GetUsersByRole;
using EduCenter.API.Features.Users.GetUserDetailsById;

namespace EduCenter.API.Features.Users;
[Authorize]
public class UserController : BaseApiController
{
    private readonly IMediator _mediator;
    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost("create")]
    [ProducesResponseType(typeof(UserViewModel), StatusCodes.Status201Created)]
    public async Task<IActionResult> Create([FromBody] RegisterUserCommand cmd, CancellationToken ct)
    {
        var user = await _mediator.Send(cmd, ct);
        return Ok();
    }
    [HttpPost("get-paged")]
    [ProducesResponseType(typeof(PagedResult<UserViewModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUsersPaged(PagedRequest<UserFilter> request, CancellationToken ct)
    {
        PagedResult<UserViewModel> result = await _mediator.Send(new GetUsersPagedQuery(request), ct);
        return Ok(result);
    }
    [HttpGet("get-all")]
    [ProducesResponseType(typeof(List<UserViewModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllUsers(CancellationToken ct)
    {
        List<UserViewModel> users = await _mediator.Send(new GetAllUsersQuery(), ct);
        return Ok(users);
    }
    [HttpGet("get-by-id/{id}")]
    [ProducesResponseType(typeof(UserViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id, CancellationToken ct)
    {
        var query = new GetUserByIdQuery(id);
        UserViewModel user = await _mediator.Send(query, ct);
        return user is null ? NotFound() : Ok(user);
    }
    [HttpGet("get-details-by-id/{id}")]
    [ProducesResponseType(typeof(UserDetailsViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCurrentUserDetailsById(int id, CancellationToken ct)
    {
        var query = new GetUserDetailsByIdQuery(id);
        UserDetailsViewModel user = await _mediator.Send(query, ct);
        return user is null ? NotFound() : Ok(user);
    }
    [HttpGet("get-current-details")]
    [ProducesResponseType(typeof(UserDetailsViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCurrentUserDetailsById(CancellationToken ct)
    {
        var userId = GetSenderId();
        var query = new GetUserDetailsByIdQuery(userId);
        UserDetailsViewModel user = await _mediator.Send(query, ct);
        return user is null ? NotFound() : Ok(user);
    }
    [HttpPost("assign-role")]
    public async Task<IActionResult> AssignRole(AssignRoleCommand request, CancellationToken ct)
    {
        await _mediator.Send(request, ct);
        return Ok();
    }
    [HttpGet("get-by-role/{id}")]
    public async Task<IActionResult> GetUsersByRole(int id, CancellationToken ct)
    {
        var users = await _mediator.Send(new GetUsersByRoleQuery(id), ct);
        return Ok(users);
    }
}
