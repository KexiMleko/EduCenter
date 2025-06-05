
using Dapper;
using EduCenter.API.Data;
using EduCenter.API.Features.Users.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EduCenter.API.Features.Users.GetUsersByRole;
public sealed record GetUsersByRoleQuery(int roleId) : IRequest<List<UserViewModel>>;
public class GetUsersByRoleHandler : IRequestHandler<GetUsersByRoleQuery, List<UserViewModel>>
{
    private readonly DatabaseContext _appContext;
    public GetUsersByRoleHandler(DatabaseContext appContext)
    {
        _appContext = appContext;
    }
    public async Task<List<UserViewModel>> Handle(GetUsersByRoleQuery request, CancellationToken ct)
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
            u.note
        FROM users u
        INNER JOIN user_roles ur ON u.id = ur.user_id
        WHERE ur.role_id = @RoleId";

        var command = new CommandDefinition(
            sql,
            new { RoleId = request.roleId },
            cancellationToken: ct
        );

        var users = await _appContext.Database.GetDbConnection()
            .QueryAsync<UserViewModel>(command);

        return users.ToList();
    }
}
