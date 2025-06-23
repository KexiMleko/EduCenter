using EduCenter.API.Base;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using EduCenter.API.Data.Models;
namespace EduCenter.API.Features.Schedules;
public class ScheduleController : BaseApiController
{
    IMediator _mediator;
    public ScheduleController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("get-current")]
    public async Task<IActionResult> GetCurrentUserSchedule(CancellationToken ct)
    {
        var request = new GetTeacherScheduleQuery(GetSenderId());
        var result = await _mediator.Send(request, ct);
        return Ok(result);
    }
}
