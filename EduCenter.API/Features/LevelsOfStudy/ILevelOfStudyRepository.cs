using EduCenter.API.Data.Models;

namespace EduCenter.API.Features.LevelOfStudys;
public interface ILevelOfStudyRepository
{
    void AddLevelOfStudy(LevelOfStudy levelOfStudy);
    void UpdateLevelOfStudy(LevelOfStudy levelOfStudy);
}
