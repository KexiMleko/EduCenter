using EduCenter.API.Data.Models;
using EduCenter.API.Features.Attendances;

namespace EduCenter.API.Data.Repositories;
public class AttendanceRepository : IAttendanceRepository
{
    DatabaseContext _appContext;
    public AttendanceRepository(DatabaseContext appContext)
    {
        _appContext = appContext;
    }
    public void AddAttendance(Attendance attendance)
    {
        _appContext.Add(attendance);
    }

    public void AddAttendances(List<Attendance> attendaces)
    {
        _appContext.AddRange(attendaces);
    }

    public void UpdateAttendance(Attendance attendance)
    {
        _appContext.Update(attendance);
    }
}
