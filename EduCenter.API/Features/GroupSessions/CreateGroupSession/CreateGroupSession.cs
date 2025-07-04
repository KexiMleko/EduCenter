using EduCenter.API.Data.Enums;
using EduCenter.API.Data.Models;
using MediatR;

namespace EduCenter.API.Features.GroupSessions.CreateGroupSession;
public sealed record CreateGroupSessionCommand(
    string Title,
    string? Description,
    int TeacherId,
    int GroupId,
    DateTime TimeScheduled,
    int ClassroomId,
    SessionStatus Status,
    int SessionDuration
) : IRequest<Unit>;

public class CreateGroupSessionHandler : IRequestHandler<CreateGroupSessionCommand, Unit>
{
    IUnitOfWork _uow;
    public CreateGroupSessionHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<Unit> Handle(CreateGroupSessionCommand request, CancellationToken cancellationToken)
    {
        _uow.groupSessions.AddGroupSession(new GroupSession
        {
            Title = request.Title,
            Description = request.Description,
            TeacherId = request.TeacherId,
            GroupId = request.GroupId,
            Status = request.Status,
            TimeScheduled = request.TimeScheduled,
            ClassroomId = request.ClassroomId,
            SessionDuration = request.SessionDuration,
            CreatedAt = DateTime.UtcNow
        });
        await _uow.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
