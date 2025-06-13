
using EduCenter.API.Base;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using EduCenter.API.Features.IndividualSessions.CreateIndividualSession;
using EduCenter.API.Features.IndividualSessions.UpdateIndividualSession;
using EduCenter.API.Data.Models;
using EduCenter.API.Features.IndividualSessions.GetAllIndividualSessions;
namespace EduCenter.API.Features.IndividualSessions;
public class IndividualSessionController : BaseApiController
{
    IMediator _mediator;
    public IndividualSessionController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost("create")]
    public async Task<IActionResult> CreateIndividualSession(CreateIndividualSessionCommand request, CancellationToken ct)
    {
        await _mediator.Send(request, ct);
        return Ok();
    }
    [HttpPost("update")]
    public async Task<IActionResult> UpdateIndividualSession(UpdateIndividualSessionCommand request, CancellationToken ct)
    {
        await _mediator.Send(request, ct);
        return Ok();
    }
    [HttpGet("get-all")]
    public async Task<IActionResult> GetAllIndividualSessions(CancellationToken ct)
    {
        var request = new GetAllIndividualSessionsQuery();
        var result = await _mediator.Send(request, ct);
        return Ok(result);
    }
}
