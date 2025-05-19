

using EduCenter.API.Data;
using EduCenter.API.Data.Models;
using EduCenter.API.Features.Subjects;

namespace EduCenter.API.Base.Repositories;
public class SubjectRepository : ISubjectRepository
{
    private readonly DatabaseContext _appContext;
    public SubjectRepository(DatabaseContext appContext)
    {
        _appContext = appContext;
    }

    public void AddSubject(Subject subject)
    {
        _appContext.Subjects.Add(subject);
    }

    public void UpdateSubject(Subject subject)
    {
        _appContext.Subjects.Update(subject);
    }
}
