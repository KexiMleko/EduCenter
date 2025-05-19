
using EduCenter.API.Data.Models;

namespace EduCenter.API.Features.Subjects;
public interface ISubjectRepository
{
    void AddSubject(Subject subject);
    void UpdateSubject(Subject subject);
}
