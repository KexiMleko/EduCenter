using Dapper;
using EduCenter.API.Data;
using EduCenter.API.Features.Users.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EduCenter.API.Features.Users.GetUserDetailsById;
public sealed record GetUserDetailsByIdQuery(int UserId) : IRequest<UserDetailsViewModel>;
public class GetUserDetailsByIdHandler : IRequestHandler<GetUserDetailsByIdQuery, UserDetailsViewModel>
{
    private readonly DatabaseContext _appContext;
    public GetUserDetailsByIdHandler(DatabaseContext appContext)
    {
        _appContext = appContext;
    }
    public async Task<UserDetailsViewModel> Handle(GetUserDetailsByIdQuery request, CancellationToken ct)
    {
        var sql = @"
SELECT 
    u.id,
    u.username,
    u.email,
    u.first_name AS FirstName,
    u.last_name AS LastName,
    u.phone_number AS PhoneNumber,
    u.address,
    u.note,
    u.created_at AS CreatedAt,
    u.updated_at AS UpdatedAt,
    json_agg(json_build_object(
    'Id', r.id,
    'Name', r.name,
    'CreatedAt', r.created_at,
    'UpdatedAt', r.updated_at
  ))::text AS UserRolesJson
FROM users u
LEFT JOIN user_roles ur ON u.id = ur.user_id
LEFT JOIN roles r ON r.id = ur.role_id
WHERE u.id = @Id
GROUP BY u.id;
";
        var command = new CommandDefinition(
            sql,
            new { Id = request.UserId },
            cancellationToken: ct
        );

        var user = await _appContext.Database.GetDbConnection()
            .QuerySingleOrDefaultAsync<UserDetailsViewModel>(command);
        Console.WriteLine(user.UserRolesJson);
        foreach (var role in user.UserRoles)
        {
            Console.WriteLine(role.Name);
        }
        if (user is null) throw new Exception("User not found");
        return user;
    }
}
