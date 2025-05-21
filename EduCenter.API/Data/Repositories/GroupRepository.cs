
using EduCenter.API.Data;
using EduCenter.API.Data.Models;
using EduCenter.API.Features.Groups;

namespace EduCenter.API.Base.Repositories;
public class GroupRepository : IGroupRepository
{
    private readonly DatabaseContext _appContext;
    public GroupRepository(DatabaseContext appContext)
    {
        _appContext = appContext;
    }

    public Group AddGroup(Group group)
    {
        _appContext.Groups.Add(group);
        return group;
    }

    public void UpdateGroup(Group group)
    {
        _appContext.Groups.Update(group);
    }
}
