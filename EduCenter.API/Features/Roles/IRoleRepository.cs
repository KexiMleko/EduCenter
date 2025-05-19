using EduCenter.API.Data.Models;

namespace EduCenter.API.Features.Roles;
public interface IRoleRepository
{
    void AddRole(Role role);
    void UpdateRole(Role role);
}
