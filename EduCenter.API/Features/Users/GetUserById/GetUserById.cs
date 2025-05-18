using Dapper;
using EduCenter.API.Data;
using EduCenter.API.Features.Users.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EduCenter.API.Features.Users.GetUserById;
public sealed record GetUserByIdQuery(int UserId) : IRequest<UserViewModel>;
public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, UserViewModel>
{
    private readonly DatabaseContext _appContext;
    public GetUserByIdHandler(DatabaseContext appContext)
    {
        _appContext = appContext;
    }
    public async Task<UserViewModel> Handle(GetUserByIdQuery request, CancellationToken ct)
    {
        var sql = @"SELECT id, username, email, first_name, last_name, phone_number, address, note 
                    FROM users 
                    WHERE id = @id";

        var command = new CommandDefinition(
            sql,
            new { Id = request.UserId },
            cancellationToken: ct
        );

        return await _appContext.Database.GetDbConnection()
            .QuerySingleOrDefaultAsync<UserViewModel>(command);
    }
}
