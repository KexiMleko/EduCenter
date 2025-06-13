
using EduCenter.API.Data.Models;

namespace EduCenter.API.Features.GroupSessions;
public interface IGroupSessionRepository
{
    void AddGroupSession(GroupSession groupSession);
    void UpdateGroupSession(GroupSession groupSession);
}
