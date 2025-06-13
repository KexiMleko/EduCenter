

using EduCenter.API.Data;
using EduCenter.API.Data.Models;
using EduCenter.API.Features.IndividualSessions;

namespace EduCenter.API.Data.Repositories;
public class IndividualSessionRepository : IIndividualSessionRepository
{
    private readonly DatabaseContext _appContext;
    public IndividualSessionRepository(DatabaseContext appContext)
    {
        _appContext = appContext;
    }

    public void AddIndividualSession(IndividualSession individualSession)
    {
        _appContext.IndividualSessions.Add(individualSession);
    }

    public void UpdateIndividualSession(IndividualSession individualSession)
    {
        _appContext.IndividualSessions.Update(individualSession);
    }
}
