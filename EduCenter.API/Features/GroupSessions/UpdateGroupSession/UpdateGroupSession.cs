
using EduCenter.API.Data.Enums;
using EduCenter.API.Data.Models;
using MediatR;

namespace EduCenter.API.Features.GroupSessions.UpdateGroupSession;
public sealed record UpdateGroupSessionCommand(
    int GroupSessionId,
    string Title,
    string? Description,
    int TeacherId,
    int GroupId,
    SessionStatus Status
) : IRequest<Unit>;

public class UpdateGroupSessionHandler : IRequestHandler<UpdateGroupSessionCommand, Unit>
{
    IUnitOfWork _uow;
    public UpdateGroupSessionHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<Unit> Handle(UpdateGroupSessionCommand request, CancellationToken cancellationToken)
    {
        _uow.groupSessions.AddGroupSession(new GroupSession
        {
            Id = request.GroupSessionId,
            Title = request.Title,
            Description = request.Description,
            TeacherId = request.TeacherId,
            GroupId = request.GroupId,
            Status = request.Status,
            UpdatedAt = DateTime.UtcNow
        });
        await _uow.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
