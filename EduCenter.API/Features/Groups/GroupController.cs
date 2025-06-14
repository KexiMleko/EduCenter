using EduCenter.API.Base;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using EduCenter.API.Features.Groups.CreateGroup;
using EduCenter.API.Features.Groups.UpdateGroup;
using EduCenter.API.Features.Groups.GetAllGroups;
using EduCenter.API.Features.Groups.GetGroupsPaged;
using EduCenter.API.Shared.Filters;
using EduCenter.API.Features.Groups.GetGroupById;
using EduCenter.API.Features.Groups.GetAllGroupsBrief;
namespace EduCenter.API.Features.Groups;
public class GroupController : BaseApiController
{
    IMediator _mediator;
    public GroupController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost("create")]
    public async Task<IActionResult> CreateGroup(CreateGroupCommand request, CancellationToken ct)
    {
        await _mediator.Send(request, ct);
        return Ok();
    }
    [HttpPost("update")]
    public async Task<IActionResult> UpdateGroup(UpdateGroupCommand request, CancellationToken ct)
    {
        await _mediator.Send(request, ct);
        return Ok();
    }
    [HttpGet("get-all")]
    public async Task<IActionResult> GetAllGroups(CancellationToken ct)
    {
        var request = new GetAllGroupsQuery();
        var result = await _mediator.Send(request, ct);
        return Ok(result);
    }
    [HttpGet("get-all-brief")]
    public async Task<IActionResult> GetAllGroupsBrief(CancellationToken ct)
    {
        var request = new GetAllGroupsBriefQuery();
        var result = await _mediator.Send(request, ct);
        return Ok(result);
    }
    [HttpPost("get-paged")]
    public async Task<IActionResult> GetGroupsPaged(PagedRequest<GroupFilter> request, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetGroupsPagedQuery(request), ct);
        return Ok(result);
    }
    [HttpGet("details/{id}")]
    public async Task<IActionResult> GetGroupDetailsById(int id, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetGroupDetailsByIdQuery(id), ct);
        return Ok(result);
    }
}
