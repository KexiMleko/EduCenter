using EduCenter.API.Data;
using EduCenter.API.Data.Models;
using EduCenter.API.Features.Students;

namespace EduCenter.API.Data.Repositories;
public class StudentRepository : IStudentRepository
{
    private readonly DatabaseContext _appContext;
    public StudentRepository(DatabaseContext appContext)
    {
        _appContext = appContext;
    }

    public void AddStudent(Student student)
    {
        _appContext.Students.Add(student);
    }

    public void UpdateStudent(Student student)
    {
        _appContext.Students.Update(student);
    }
}
