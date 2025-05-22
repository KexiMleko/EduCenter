
using EduCenter.API.Data.Enums;
using EduCenter.API.Data.Models;
using MediatR;

namespace EduCenter.API.Features.Attendances.CreateAttendance;

public sealed record CreateAttendanceCommand(
    int SessionId,
    int StudentId,
    bool WasPresent,
    StudentPerformance Performance,
    string? Note
) : IRequest<Unit>;
public class CreateAttendanceHandler : IRequestHandler<CreateAttendanceCommand, Unit>
{
    IUnitOfWork _uow;
    public CreateAttendanceHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<Unit> Handle(CreateAttendanceCommand request, CancellationToken cancellationToken)
    {
        _uow.attendances.AddAttendance(new Attendance
        {
            SessionId = request.SessionId,
            StudentId = request.StudentId,
            WasPresent = request.WasPresent,
            Performance = request.Performance,
            Note = request.Note
        });
        await _uow.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
