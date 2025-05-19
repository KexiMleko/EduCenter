
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
    public async Task<IActionResult> CreateSubject(CreateSubjectCommand request)
    {
        await _mediator.Send(request);
        return Ok();
    }
    [HttpPost("update-subject")]
    public async Task<IActionResult> UpdateSubject(UpdateSubjectCommand request)
    {
        await _mediator.Send(request);
        return Ok();
    }
    [HttpGet("get-all-subjects")]
    public async Task<IActionResult> GetAllSubjects()
    {
        var request = new GetAllSubjectsQuery();
        var result = await _mediator.Send(request);
        return Ok(result);
    }
}
