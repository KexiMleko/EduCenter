using EduCenter.API.Data.Models;
using EduCenter.API.Features.Users.DTOs;
namespace EduCenter.API.Features.Users;
public interface IUserRepository
{
    public Task<User> AddUser(User user, CancellationToken ct);
    public Task<User> GetUserByUsername(string username, CancellationToken ct);
    public Task UpdateRefreshToken(RefreshToken token, CancellationToken ct);
}
