using EduCenter.API.Data;
using EduCenter.API.Data.Models;
using EduCenter.API.Features.Users;
using EduCenter.API.Features.Users.DTOs;

namespace EduCenter.API.Base.Repositories;
public class UserRepository : BaseRepository, IUserRepository
{
    public UserRepository(DatabaseContext appContext) : base(appContext) { }
    public async Task<UserViewModel> AddUser(User user, CancellationToken ct)
    {
        Console.WriteLine($"repo test: {user.Username}");
        await appContext.Users.AddAsync(user, ct);

        Console.WriteLine($"repo test2: {user.Username}");
        return new UserViewModel
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            PhoneNumber = user.PhoneNumber,
            Address = user.Address,
            Note = user.Note
        };
    }
}
