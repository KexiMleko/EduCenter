using MediatR;
using EduCenter.API.Data.Models;
using EduCenter.API.Shared.Services.Hashing;
using EduCenter.API.Features.Users.DTOs;

namespace EduCenter.API.Features.Users.RegisterUser;

public sealed record RegisterUserCommand(
        string Username,
        string Email,
        string Password,
        string FirstName,
        string LastName,
        string PhoneNumber,
        string Address,
        string? Note) : IRequest<UserViewModel>;

public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, UserViewModel>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHashService _hasher;
    public RegisterUserHandler(IUserRepository userRepository, IPasswordHashService hasher)
    {
        _userRepository = userRepository;
        _hasher = hasher;
    }
    public async Task<UserViewModel> Handle(RegisterUserCommand request, CancellationToken ct)
    {
        var user = new User
        {
            Username = request.Username,
            Email = request.Email,
            PasswordHash = _hasher.HashPassword(request.Password),
            FirstName = request.FirstName,
            LastName = request.LastName,
            PhoneNumber = request.PhoneNumber,
            Address = request.Address,
            Note = request.Note,
            CreatedAt = DateTime.UtcNow,
            IsActive = true
        };
        var userView = await _userRepository.AddUser(user, ct);
        await _userRepository.SaveChangesAsync(ct);
        return userView;
    }
}

