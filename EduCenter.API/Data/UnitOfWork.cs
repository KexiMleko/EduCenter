
using EduCenter.API.Base.Repositories;
using EduCenter.API.Features.Roles;
using EduCenter.API.Features.Subjects;
using EduCenter.API.Features.Users;

namespace EduCenter.API.Data;
public class UnitOfWork : IUnitOfWork
{
    DatabaseContext _appContext;
    public UnitOfWork(DatabaseContext appContext)
    {
        _appContext = appContext;
    }
    IUserRepository _users;
    IRoleRepository _roles;
    ISubjectRepository _subjects;
    // NOTE: Getters
    public IUserRepository users
    {
        get => _users != null ? _users : new UserRepository(_appContext);
    }
    public IRoleRepository roles
    {
        get => _roles != null ? _roles : new RoleRepository(_appContext);
    }
    public ISubjectRepository subjects
    {
        get => _subjects != null ? _subjects : new SubjectRepository(_appContext);
    }
    // NOTE: Save changes
    public async Task SaveChangesAsync(CancellationToken ct)
    {
        await _appContext.SaveChangesAsync(ct);
    }
}
