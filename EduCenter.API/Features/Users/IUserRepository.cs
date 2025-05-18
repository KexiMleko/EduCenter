using EduCenter.API.Data.Models;
using EduCenter.API.Features.Users.DTOs;
namespace EduCenter.API.Features.Users;
public interface IUserRepository : IBaseRepository
{
    public Task<User> AddUser(User user, CancellationToken ct);
}
