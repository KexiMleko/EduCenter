
using EduCenter.API.Data.Models;
using MediatR;

namespace EduCenter.API.Features.Groups.UpdateGroup;
public sealed record UpdateGroupCommand(int groupId, int teacherId, int subjectId, int maxNumberOfClasses, int numberOfClassesLeft) : IRequest<Unit>;
public class UpdateGroupHandler : IRequestHandler<UpdateGroupCommand, Unit>
{
    IUnitOfWork _uow;
    public UpdateGroupHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<Unit> Handle(UpdateGroupCommand request, CancellationToken cancellationToken)
    {
        _uow.groups.UpdateGroup(new Group
        {
            TeacherId = request.teacherId,
            SubjectId = request.subjectId,
            MaxNumberOfClasses = request.maxNumberOfClasses,
            NumberOfClassesLeft = request.numberOfClassesLeft,
            UpdatedAt = DateTime.UtcNow,
        });
        await _uow.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
