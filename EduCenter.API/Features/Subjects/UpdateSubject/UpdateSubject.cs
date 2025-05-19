
using EduCenter.API.Data.Models;
using MediatR;

namespace EduCenter.API.Features.Subjects.UpdateSubject;
public sealed record UpdateSubjectCommand(int subjectId, string name, string description) : IRequest<Unit>;
public class UpdateSubjectHandler : IRequestHandler<UpdateSubjectCommand, Unit>
{
    IUnitOfWork _uow;
    public UpdateSubjectHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<Unit> Handle(UpdateSubjectCommand request, CancellationToken cancellationToken)
    {
        _uow.subjects.UpdateSubject(new Subject
        {
            Id = request.subjectId,
            Name = request.name,
            Description = request.description,
            UpdatedAt = DateTime.UtcNow,
        });
        await _uow.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
