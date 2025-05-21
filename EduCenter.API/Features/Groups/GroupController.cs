using EduCenter.API.Base;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using EduCenter.API.Features.Groups.CreateGroup;
using EduCenter.API.Features.Groups.UpdateGroup;
using EduCenter.API.Features.Groups.GetAllGroups;
namespace EduCenter.API.Features.Groups;
public class GroupController : BaseApiController
{
    IMediator _mediator;
    public GroupController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost("create-role")]
    public async Task<IActionResult> CreateGroup(CreateGroupCommand request)
    {
        await _mediator.Send(request);
        return Ok();
    }
    [HttpPost("update-role")]
    public async Task<IActionResult> UpdateGroup(UpdateGroupCommand request)
    {
        await _mediator.Send(request);
        return Ok();
    }
    [HttpGet("get-all")]
    public async Task<IActionResult> GetAllGroups()
    {
        var request = new GetAllGroupsQuery();
        var result = await _mediator.Send(request);
        return Ok(result);
    }
}
