using EduCenter.API.Data.Models;
namespace EduCenter.API.Features.Attendances;
public interface IAttendanceRepository
{
    void AddAttendances(List<Attendance> attendaces);
    void AddAttendance(Attendance attendance);
    void UpdateAttendance(Attendance attendance);
}
