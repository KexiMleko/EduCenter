using EduCenter.API.Data.Models;
using MediatR;

namespace EduCenter.API.Features.Classrooms.CreateClassroom;
public sealed record CreateClassroomCommand(string name) : IRequest<Unit>;
public class CreateClassroomHandler : IRequestHandler<CreateClassroomCommand, Unit>
{
    IUnitOfWork _uow;
    public CreateClassroomHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<Unit> Handle(CreateClassroomCommand request, CancellationToken cancellationToken)
    {
        _uow.classrooms.AddClassroom(new Classroom
        {
            Name = request.name,
            CreatedAt = DateTime.UtcNow
        });
        await _uow.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
