using EduCenter.API.Base;
using EduCenter.API.Features.Students.CreateStudent;
using EduCenter.API.Features.Students.GetAllStudents;
using EduCenter.API.Features.Students.UpdateStudent;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EduCenter.API.Features.Students;
public class StudentController : BaseApiController
{
    private readonly IMediator _mediator;
    public StudentController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost("add")]
    public IActionResult CreateStudent()
    {
        return Ok();
    }
    [HttpPost("create")]
    public async Task<IActionResult> CreateStudent(CreateStudentCommand request, CancellationToken ct)
    {
        await _mediator.Send(request, ct);
        return Ok();
    }
    [HttpPost("update")]
    public async Task<IActionResult> UpdateStudent(UpdateStudentCommand request, CancellationToken ct)
    {
        await _mediator.Send(request, ct);
        return Ok();
    }
    [HttpGet("get-all")]
    public async Task<IActionResult> GetAllStudents(CancellationToken ct)
    {
        var request = new GetAllStudentsQuery();
        var result = await _mediator.Send(request, ct);
        return Ok(result);
    }

}
