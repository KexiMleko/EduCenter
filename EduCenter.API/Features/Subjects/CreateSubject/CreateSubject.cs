using EduCenter.API.Data.Models;
using MediatR;

namespace EduCenter.API.Features.Subjects.CreateSubject;
public sealed record CreateSubjectCommand(string name, string description) : IRequest<Unit>;
public class CreateSubjectHandler : IRequestHandler<CreateSubjectCommand, Unit>
{
    IUnitOfWork _uow;
    public CreateSubjectHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<Unit> Handle(CreateSubjectCommand request, CancellationToken cancellationToken)
    {
        _uow.subjects.AddSubject(new Subject
        {
            Name = request.name,
            Description = request.description,
            CreatedAt = DateTime.UtcNow
        });
        await _uow.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
