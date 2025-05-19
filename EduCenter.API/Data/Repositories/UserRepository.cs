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
    public User AddUser(User user)
    {
        _appContext.Users.Add(user);
        return user;
    }
    public async Task<User> GetUserByUsername(string username, CancellationToken ct)
    {
        return await _appContext.Users.Where(u => u.Username == username).FirstOrDefaultAsync(ct);
    }
    public void UpdateRefreshToken(RefreshToken token)
    {
        _appContext.RefreshTokens.Add(token);
    }
    public void AddUserRole(int userId, int roleId)
    {
        var userRole = new UserRole
        {
            UserId = userId,
            RoleId = roleId
        };
        _appContext.UserRoles.Add(userRole);
    }
}
