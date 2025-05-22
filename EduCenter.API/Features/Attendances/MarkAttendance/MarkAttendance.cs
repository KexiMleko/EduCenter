
using EduCenter.API.Data.Models;
using MediatR;

namespace EduCenter.API.Features.Attendances.MarkAttendance;
public sealed record MarkAttendanceCommand(List<Attendance> attendances) : IRequest<Unit>;
public class MarkAttendanceHandler : IRequestHandler<MarkAttendanceCommand, Unit>
{
    IUnitOfWork _uow;
    public MarkAttendanceHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<Unit> Handle(MarkAttendanceCommand request, CancellationToken cancellationToken)
    {
        _uow.attendances.AddAttendances(request.attendances);
        await _uow.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
