
using EduCenter.API.Base;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using EduCenter.API.Features.GroupSessions.CreateGroupSession;
using EduCenter.API.Features.GroupSessions.UpdateGroupSession;
using EduCenter.API.Data.Models;
using EduCenter.API.Features.GroupSessions.GetAllGroupSessions;
namespace EduCenter.API.Features.GroupSessions;
public class GroupSessionController : BaseApiController
{
    IMediator _mediator;
    public GroupSessionController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost("create")]
    public async Task<IActionResult> CreateGroupSession(CreateGroupSessionCommand request, CancellationToken ct)
    {
        await _mediator.Send(request, ct);
        return Ok();
    }
    [HttpPost("update")]
    public async Task<IActionResult> UpdateGroupSession(UpdateGroupSessionCommand request, CancellationToken ct)
    {
        await _mediator.Send(request, ct);
        return Ok();
    }
    [HttpGet("get-all")]
    public async Task<IActionResult> GetAllGroupSessions(CancellationToken ct)
    {
        var request = new GetAllGroupSessionsQuery();
        var result = await _mediator.Send(request, ct);
        return Ok(result);
    }
}
