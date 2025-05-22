
using EduCenter.API.Data;
using EduCenter.API.Data.Models;
using EduCenter.API.Features.Roles;

namespace EduCenter.API.Data.Repositories;
public class RoleRepository : IRoleRepository
{
    private readonly DatabaseContext _appContext;
    public RoleRepository(DatabaseContext appContext)
    {
        _appContext = appContext;
    }

    public void AddRole(Role role)
    {
        _appContext.Roles.Add(role);
    }

    public void UpdateRole(Role role)
    {
        _appContext.Roles.Update(role);
    }
}
