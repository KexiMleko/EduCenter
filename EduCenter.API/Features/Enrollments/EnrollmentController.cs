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
    public async Task<IActionResult> EnrollStudent(EnrollStudentCommand request)
    {
        await _mediator.Send(request);
        return Ok();
    }
    [HttpPost("multiple-with-plan")]
    public async Task<IActionResult> EnrollMultipleStudentWithPlans(List<StudentEnrollmentDto> students, CancellationToken cancellationToken)
    {
        await _mediator.Send(new EnrollStudentsWithPaymentPlansCommand(students), cancellationToken);
        return Ok();
    }
}
