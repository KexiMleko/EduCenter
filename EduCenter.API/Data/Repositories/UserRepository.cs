using EduCenter.API.Data;
using EduCenter.API.Data.Models;
using EduCenter.API.Features.Users;
using Microsoft.EntityFrameworkCore;

namespace EduCenter.API.Base.Repositories;
public class UserRepository : IUserRepository
{
    private readonly DatabaseContext _appContext;
    public UserRepository(DatabaseContext appContext)
    {
        _appContext = appContext;
    }
    public async Task<User> AddUser(User user, CancellationToken ct)
    {
        await _appContext.Users.AddAsync(user, ct);
        return user;
    }
    public async Task<User> GetUserByUsername(string username, CancellationToken ct)
    {
        return await _appContext.Users.Where(u => u.Username == username).FirstOrDefaultAsync(ct);
    }
    public async Task UpdateRefreshToken(RefreshToken token, CancellationToken ct)
    {
        await _appContext.RefreshTokens.AddAsync(token, ct);
    }
}
