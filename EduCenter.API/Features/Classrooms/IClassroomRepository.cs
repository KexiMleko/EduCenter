using EduCenter.API.Data.Models;

namespace EduCenter.API.Features.Classrooms;
public interface IClassroomRepository
{
    void AddClassroom(Classroom classroom);
    void UpdateClassroom(Classroom classroom);
}
