using EduCenter.API.Base;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using EduCenter.API.Features.Roles.CreateRole;
using EduCenter.API.Features.Roles.UpdateRole;
using EduCenter.API.Data.Models;
using EduCenter.API.Features.Roles.GetAllRoles;
namespace EduCenter.API.Features.Roles;
public class RoleController : BaseApiController
{
    IMediator _mediator;
    public RoleController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost("create")]
    public async Task<IActionResult> CreateRole(CreateRoleCommand request, CancellationToken ct)
    {
        await _mediator.Send(request, ct);
        return Ok();
    }
    [HttpPost("update")]
    public async Task<IActionResult> UpdateRole(UpdateRoleCommand request, CancellationToken ct)
    {
        await _mediator.Send(request, ct);
        return Ok();
    }
    [HttpGet("get-all")]
    public async Task<IActionResult> GetAllRoles(CancellationToken ct)
    {
        var request = new GetAllRolesQuery();
        var result = await _mediator.Send(request, ct);
        return Ok(result);
    }
}
