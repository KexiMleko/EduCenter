using EduCenter.API.Data.Enums;
using EduCenter.API.Data.Models;
using MediatR;

namespace EduCenter.API.Features.IndividualSessions.CreateIndividualSession;
public sealed record CreateIndividualSessionCommand(
    string Title,
    string? Description,
    int TeacherId,
    int SubjectId,
    DateTime TimeScheduled,
    int ClassroomId,
    SessionStatus Status,
    int SessionDuration
) : IRequest<Unit>;

public class CreateIndividualSessionHandler : IRequestHandler<CreateIndividualSessionCommand, Unit>
{
    IUnitOfWork _uow;
    public CreateIndividualSessionHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<Unit> Handle(CreateIndividualSessionCommand request, CancellationToken cancellationToken)
    {
        _uow.individualSessions.AddIndividualSession(new IndividualSession
        {
            Title = request.Title,
            Description = request.Description,
            TeacherId = request.TeacherId,
            SubjectId = request.SubjectId,
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
