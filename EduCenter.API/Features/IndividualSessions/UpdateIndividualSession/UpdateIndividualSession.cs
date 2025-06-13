
using EduCenter.API.Data.Enums;
using EduCenter.API.Data.Models;
using MediatR;

namespace EduCenter.API.Features.IndividualSessions.UpdateIndividualSession;
public sealed record UpdateIndividualSessionCommand(
    int IndividualSessionId,
    string Title,
    string? Description,
    int TeacherId,
    int SessionDuration,
    int SubjectId,
    SessionStatus Status
) : IRequest<Unit>;

public class UpdateIndividualSessionHandler : IRequestHandler<UpdateIndividualSessionCommand, Unit>
{
    IUnitOfWork _uow;
    public UpdateIndividualSessionHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<Unit> Handle(UpdateIndividualSessionCommand request, CancellationToken cancellationToken)
    {
        _uow.individualSessions.AddIndividualSession(new IndividualSession
        {
            Id = request.IndividualSessionId,
            Title = request.Title,
            Description = request.Description,
            TeacherId = request.TeacherId,
            SubjectId = request.SubjectId,
            Status = request.Status,
            SessionDuration = request.SessionDuration,
            UpdatedAt = DateTime.UtcNow
        });
        await _uow.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
