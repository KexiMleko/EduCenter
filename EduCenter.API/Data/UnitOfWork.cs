
using EduCenter.API.Base.Repositories;
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
    // NOTE: Getters
    public IUserRepository users
    {
        get
        {
            return _users != null ? _users : new UserRepository(_appContext);
        }
    }
    // NOTE: Save changes
    public async Task SaveChangesAsync(CancellationToken ct)
    {
        await _appContext.SaveChangesAsync(ct);
    }
}
