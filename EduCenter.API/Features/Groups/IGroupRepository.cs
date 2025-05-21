using EduCenter.API.Data.Models;

namespace EduCenter.API.Features.Groups;
public interface IGroupRepository
{
    Group AddGroup(Group group);
    void UpdateGroup(Group group);
}
