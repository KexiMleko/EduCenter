using EduCenter.API.Base;
using EduCenter.API.Features.Enrollments.EnrollStudent;
using Microsoft.AspNetCore.Mvc;
using MediatR;

public class EnrollmentController : BaseApiController
{
    private readonly IMediator _mediator;
    public EnrollmentController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost("enroll-student")]
    public IActionResult EnrollStudent(EnrollStudentCommand request)
    {
        _mediator.Send(request);
        return Ok();
    }
}
