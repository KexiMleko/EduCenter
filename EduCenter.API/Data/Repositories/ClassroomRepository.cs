
using EduCenter.API.Data;
using EduCenter.API.Data.Models;
using EduCenter.API.Features.Classrooms;

namespace EduCenter.API.Data.Repositories;
public class ClassroomRepository : IClassroomRepository
{
    private readonly DatabaseContext _appContext;
    public ClassroomRepository(DatabaseContext appContext)
    {
        _appContext = appContext;
    }

    public void AddClassroom(Classroom classroom)
    {
        _appContext.Classrooms.Add(classroom);
    }

    public void UpdateClassroom(Classroom classroom)
    {
        _appContext.Classrooms.Update(classroom);
    }
}
