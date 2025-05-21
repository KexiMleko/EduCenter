
using EduCenter.API.Data.Models;
using MediatR;

namespace EduCenter.API.Features.LevelsOfStudy.UpdateLevelOfStudy;
public sealed record UpdateLevelOfStudyCommand(int roleId, string title, string description) : IRequest<Unit>;
public class UpdateLevelOfStudyHandler : IRequestHandler<UpdateLevelOfStudyCommand, Unit>
{
    IUnitOfWork _uow;
    public UpdateLevelOfStudyHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<Unit> Handle(UpdateLevelOfStudyCommand request, CancellationToken cancellationToken)
    {
        _uow.levelsOfStudy.UpdateLevelOfStudy(new LevelOfStudy
        {
            Id = request.roleId,
            Title = request.title,
            Description = request.description,
            UpdatedAt = DateTime.UtcNow,
        });
        await _uow.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
