
using EduCenter.API.Base;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using EduCenter.API.Features.Subjects.CreateSubject;
using EduCenter.API.Features.Subjects.UpdateSubject;
using EduCenter.API.Features.Subjects.GetAllSubjects;
namespace EduCenter.API.Features.Subjects;
public class SubjectController : BaseApiController
{
    IMediator _mediator;
    public SubjectController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost("create-subject")]
    public async Task<IActionResult> CreateSubject(CreateSubjectCommand request, CancellationToken ct)
    {
        await _mediator.Send(request, ct);
        return Ok();
    }
    [HttpPost("update-subject")]
    public async Task<IActionResult> UpdateSubject(UpdateSubjectCommand request, CancellationToken ct)
    {
        await _mediator.Send(request, ct);
        return Ok();
    }
    [HttpGet("get-all")]
    public async Task<IActionResult> GetAllSubjects(CancellationToken ct)
    {
        var request = new GetAllSubjectsQuery();
        var result = await _mediator.Send(request, ct);
        return Ok(result);
    }
}
