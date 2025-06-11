using EduCenter.API.Data.Models;
namespace EduCenter.API.Features.Users;
public interface IUserRepository
{
    public void AddUser(User user);
    public Task<User> GetUserByUsername(string username, CancellationToken ct);
    public void UpdateRefreshToken(RefreshToken token);
    public void AddUserRole(int userId, int roleId);
    public Task<User> GetUserByRefreshToken(string token);
    public void DeleteRefreshToken(string token);
    public void AddUserRoles(int userId, List<int> roleIds);
}
