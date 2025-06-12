
using EduCenter.API.Data.Models;
using MediatR;

namespace EduCenter.API.Features.Classrooms.UpdateClassroom;
public sealed record UpdateClassroomCommand(int roleId, string name) : IRequest<Unit>;
public class UpdateClassroomHandler : IRequestHandler<UpdateClassroomCommand, Unit>
{
    IUnitOfWork _uow;
    public UpdateClassroomHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<Unit> Handle(UpdateClassroomCommand request, CancellationToken cancellationToken)
    {
        _uow.classrooms.UpdateClassroom(new Classroom
        {
            Id = request.roleId,
            Name = request.name,
            UpdatedAt = DateTime.UtcNow,
        });
        await _uow.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
