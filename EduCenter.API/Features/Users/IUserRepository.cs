using EduCenter.API.Data.Models;
namespace EduCenter.API.Features.Users;
public interface IUserRepository
{
    public User AddUser(User user);
    public Task<User> GetUserByUsername(string username, CancellationToken ct);
    public void UpdateRefreshToken(RefreshToken token);
    public void AddUserRole(int userId, int roleId);
}
