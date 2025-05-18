using EduCenter.API.Data;
using EduCenter.API.Data.Models;
using EduCenter.API.Features.Users;
using EduCenter.API.Features.Users.DTOs;

namespace EduCenter.API.Base.Repositories;
public class UserRepository : BaseRepository, IUserRepository
{
    public UserRepository(DatabaseContext appContext) : base(appContext) { }
    public async Task<User> AddUser(User user, CancellationToken ct)
    {
        await appContext.Users.AddAsync(user, ct);

        Console.WriteLine($"repo test2: {user.Id}");
        return user;
    }
}
