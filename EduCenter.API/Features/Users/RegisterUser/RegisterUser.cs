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
        string? Note,
        List<int> RoleIds
        ) : IRequest<Unit>;

public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, Unit>
{
    private readonly IUnitOfWork _uow;
    private readonly IPasswordHashService _hasher;
    public RegisterUserHandler(IUnitOfWork uow, IPasswordHashService hasher)
    {
        _uow = uow;
        _hasher = hasher;
    }
    public async Task<Unit> Handle(RegisterUserCommand request, CancellationToken ct)
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
        user = _uow.users.AddUser(user);

        if (request.RoleIds.Count > 0)
        {
            _uow.users.AddUserRoles(user.Id, request.RoleIds);
        }
        await _uow.SaveChangesAsync(ct);
        return Unit.Value;
    }
}

