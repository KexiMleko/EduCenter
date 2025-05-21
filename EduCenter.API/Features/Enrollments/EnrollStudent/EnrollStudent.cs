using EduCenter.API.Data.Models;
using MediatR;

namespace EduCenter.API.Features.Enrollments.EnrollStudent;
public sealed record EnrollStudentCommand(int studentId, int groupId, int paymentPlanId) : IRequest<Unit>;
public class EnrollStudentHandler : IRequestHandler<EnrollStudentCommand, Unit>
{
    IUnitOfWork _uow;
    public EnrollStudentHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<Unit> Handle(EnrollStudentCommand request, CancellationToken cancellationToken)
    {
        _uow.enrollments.AddEnrollment(new Enrollment { StudentId = request.studentId, GroupId = request.groupId, PaymentPlanId = request.paymentPlanId });
        await _uow.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
