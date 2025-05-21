using EduCenter.API.Data.Models;
using MediatR;

namespace EduCenter.API.Features.Groups.CreateGroup;
public sealed record CreateGroupCommand(int teacherId, int subjectId, int maxNumberOfClasses, int numberOfClassesLeft, Dictionary<int, int> studentPaymentPlans) : IRequest<Unit>;
public class CreateGroupHandler : IRequestHandler<CreateGroupCommand, Unit>
{
    IUnitOfWork _uow;
    public CreateGroupHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<Unit> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
    {
        var group = _uow.groups.AddGroup(new Group
        {
            TeacherId = request.teacherId,
            SubjectId = request.subjectId,
            MaxNumberOfClasses = request.maxNumberOfClasses,
            NumberOfClassesLeft = request.numberOfClassesLeft,
            CreatedAt = DateTime.UtcNow
        });
        if (request.studentPaymentPlans.Count > 0)
        {
            List<Enrollment> enrollments = new List<Enrollment>();
            foreach (var plan in request.studentPaymentPlans)
            {

                enrollments.Add(new Enrollment
                {
                    StudentId = plan.Key,
                    GroupId = group.Id,
                    PaymentPlanId = plan.Value
                });
            }
            _uow.enrollments.AddEnrollments(enrollments);
        }
        await _uow.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
