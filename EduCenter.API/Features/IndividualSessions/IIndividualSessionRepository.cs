
using EduCenter.API.Data.Models;

namespace EduCenter.API.Features.IndividualSessions;
public interface IIndividualSessionRepository
{
    void AddIndividualSession(IndividualSession individualSession);
    void UpdateIndividualSession(IndividualSession individualSession);
}
