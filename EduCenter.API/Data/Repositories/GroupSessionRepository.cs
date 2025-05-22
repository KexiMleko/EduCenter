

using EduCenter.API.Data;
using EduCenter.API.Data.Models;
using EduCenter.API.Features.GroupSessions;

namespace EduCenter.API.Data.Repositories;
public class GroupSessionRepository : IGroupSessionRepository
{
    private readonly DatabaseContext _appContext;
    public GroupSessionRepository(DatabaseContext appContext)
    {
        _appContext = appContext;
    }

    public void AddGroupSession(GroupSession groupSession)
    {
        _appContext.GroupSessions.Add(groupSession);
    }

    public void UpdateGroupSession(GroupSession groupSession)
    {
        _appContext.GroupSessions.Update(groupSession);
    }
}
