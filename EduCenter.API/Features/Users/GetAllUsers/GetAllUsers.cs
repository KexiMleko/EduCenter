using Dapper;
using EduCenter.API.Data;
using EduCenter.API.Features.Users.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EduCenter.API.Features.Users.GetUserById;
public sealed record GetAllUsersQuery() : IRequest<List<UserViewModel>>;
public class GetAllUsersHandler : IRequestHandler<GetAllUsersQuery, List<UserViewModel>>
{
    private readonly DatabaseContext _appContext;
    public GetAllUsersHandler(DatabaseContext appContext)
    {
        _appContext = appContext;
    }
    public async Task<List<UserViewModel>> Handle(GetAllUsersQuery request, CancellationToken ct)
    {
        var sql = @"SELECT id, username, email, first_name AS FirstName, last_name AS LastName, phone_number AS PhoneNumber, address, note 
                    FROM users";

        var command = new CommandDefinition(
            sql,
            cancellationToken: ct
        );

        var users = await _appContext.Database.GetDbConnection()
            .QueryAsync<UserViewModel>(command);
        return users.ToList();
    }
}
