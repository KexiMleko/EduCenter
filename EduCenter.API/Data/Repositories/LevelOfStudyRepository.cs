
using EduCenter.API.Data;
using EduCenter.API.Data.Models;
using EduCenter.API.Features.LevelOfStudys;

namespace EduCenter.API.Base.Repositories;
public class LevelOfStudyRepository : ILevelOfStudyRepository
{
    private readonly DatabaseContext _appContext;
    public LevelOfStudyRepository(DatabaseContext appContext)
    {
        _appContext = appContext;
    }

    public void AddLevelOfStudy(LevelOfStudy role)
    {
        _appContext.LevelsOfStudy.Add(role);
    }

    public void UpdateLevelOfStudy(LevelOfStudy role)
    {
        _appContext.LevelsOfStudy.Update(role);
    }
}
