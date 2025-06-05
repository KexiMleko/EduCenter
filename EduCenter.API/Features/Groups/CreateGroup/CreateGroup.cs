using EduCenter.API.Data.Models;
using MediatR;

namespace EduCenter.API.Features.Groups.CreateGroup;
public sealed record CreateGroupCommand(string name, int teacherId, int subjectId, int maxNumberOfClasses, Dictionary<int, int>? studentPaymentPlans) : IRequest<Unit>;
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
            Name = request.name,
            TeacherId = request.teacherId,
            SubjectId = request.subjectId,
            MaxNumberOfClasses = request.maxNumberOfClasses,
            NumberOfClassesLeft = request.maxNumberOfClasses,
            CreatedAt = DateTime.UtcNow
        });
        if (request.studentPaymentPlans != null)
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
            if (enrollments.Count > 0)
                _uow.enrollments.AddEnrollments(enrollments);
        }
        await _uow.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
