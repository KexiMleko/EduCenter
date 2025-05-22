using EduCenter.API.Data.Models;

namespace EduCenter.API.Features.Students;
public interface IStudentRepository
{
    void AddStudent(Student student);
    void UpdateStudent(Student student);
}
