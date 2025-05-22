
using EduCenter.API.Base;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using EduCenter.API.Data.Models;
using EduCenter.API.Features.Attendances.MarkAttendance;
namespace EduCenter.API.Features.Attendances;
public class AttendanceController : BaseApiController
{
    IMediator _mediator;
    public AttendanceController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost("mark")]
    public async Task<IActionResult> MarkAttendance(MarkAttendanceCommand request)
    {
        await _mediator.Send(request);
        return Ok();
    }
}
