using EduCenter.API.Data;
using EduCenter.API.Data.Models;
using EduCenter.API.Features.Users;
using Microsoft.EntityFrameworkCore;

namespace EduCenter.API.Data.Repositories;
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
        var existingToken = _appContext.RefreshTokens
        .FirstOrDefault(t => t.UserId == token.UserId);

        if (existingToken == null)
        {
            _appContext.RefreshTokens.Add(token);
        }
        else
        {
            existingToken.Token = token.Token;
            existingToken.ExpiresAt = token.ExpiresAt;
            existingToken.CreatedAt = token.CreatedAt;
        }
    }
    public void AddUserRoles(int userId, List<int> roleIds)
    {
        List<UserRole> roles = new List<UserRole>();
        foreach (var roleId in roleIds)
        {
            roles.Add(new UserRole
            {
                UserId = userId,
                RoleId = roleId
            });
        }
        _appContext.UserRoles.AddRange(roles);
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
    public async Task<User> GetUserByRefreshToken(string token)
    {
        var tokenWithUser = await _appContext.RefreshTokens.Include(t => t.User).FirstOrDefaultAsync(t => t.Token == token);
        if (tokenWithUser == null) throw new Exception("Refresh token not found!");
        if (tokenWithUser.ExpiresAt <= DateTime.UtcNow) throw new Exception("Refresh token expired!");
        return tokenWithUser.User;
    }
    public void DeleteRefreshToken(string token)
    {
        _appContext.RefreshTokens.Where(t => t.Token == token).ExecuteDelete();
    }
}
