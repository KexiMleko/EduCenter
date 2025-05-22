using EduCenter.API.Data.Enums;
using EduCenter.API.Data.Models;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace EduCenter.API.Features.Attendances.UpdateAttendance;

public sealed record UpdateAttendanceCommand(
    int Id,
    int SessionId,
    int StudentId,
    bool WasPresent,
    StudentPerformance Performance,
    string? Note
) : IRequest<Unit>;

public class UpdateAttendanceHandler : IRequestHandler<UpdateAttendanceCommand, Unit>
{
    private readonly IUnitOfWork _uow;

    public UpdateAttendanceHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<Unit> Handle(UpdateAttendanceCommand request, CancellationToken cancellationToken)
    {
        _uow.attendances.UpdateAttendance(new Attendance
        {
            Id = request.Id,
            SessionId = request.SessionId,
            StudentId = request.StudentId,
            WasPresent = request.WasPresent,
            Performance = request.Performance,
            Note = request.Note,
            UpdatedAt = DateTime.UtcNow
        });

        await _uow.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
