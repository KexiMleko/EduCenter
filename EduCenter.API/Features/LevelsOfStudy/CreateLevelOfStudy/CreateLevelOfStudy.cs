using EduCenter.API.Data.Models;
using MediatR;

namespace EduCenter.API.Features.LevelsOfStudy.CreateLevelOfStudy;
public sealed record CreateLevelOfStudyCommand(string title, string description) : IRequest<Unit>;
public class CreateLevelOfStudyHandler : IRequestHandler<CreateLevelOfStudyCommand, Unit>
{
    IUnitOfWork _uow;
    public CreateLevelOfStudyHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<Unit> Handle(CreateLevelOfStudyCommand request, CancellationToken cancellationToken)
    {
        _uow.levelsOfStudy.AddLevelOfStudy(new LevelOfStudy
        {
            Title = request.title,
            Description = request.description,
            CreatedAt = DateTime.UtcNow
        });
        await _uow.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
