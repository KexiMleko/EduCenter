using EduCenter.API.Base;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using EduCenter.API.Features.Classrooms.CreateClassroom;
using EduCenter.API.Features.Classrooms.UpdateClassroom;
using EduCenter.API.Data.Models;
using EduCenter.API.Features.Classrooms.GetAllClassrooms;
namespace EduCenter.API.Features.Classrooms;
public class ClassroomController : BaseApiController
{
    IMediator _mediator;
    public ClassroomController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost("create")]
    public async Task<IActionResult> CreateClassroom(CreateClassroomCommand request, CancellationToken ct)
    {
        await _mediator.Send(request, ct);
        return Ok();
    }
    [HttpPost("update")]
    public async Task<IActionResult> UpdateClassroom(UpdateClassroomCommand request, CancellationToken ct)
    {
        await _mediator.Send(request, ct);
        return Ok();
    }
    [HttpGet("get-all")]
    public async Task<IActionResult> GetAllClassrooms(CancellationToken ct)
    {
        var request = new GetAllClassroomsQuery();
        var result = await _mediator.Send(request, ct);
        return Ok(result);
    }
}
