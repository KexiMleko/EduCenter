using Dapper;
using EduCenter.API.Data;
using EduCenter.API.Data.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
namespace EduCenter.API.Features.Roles.GetAllRoles;
public sealed record GetAllRolesQuery() : IRequest<List<Role>>;
public class GetAllRolesHandler : IRequestHandler<GetAllRolesQuery, List<Role>>
{
    private readonly DatabaseContext _appContext;
    public GetAllRolesHandler(DatabaseContext appContext)
    {
        _appContext = appContext;
    }

    public async Task<List<Role>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
    {
        var sql = @"SELECT * FROM roles";
        var command = new CommandDefinition(
            sql,
            cancellationToken: cancellationToken
        );

        var result = await _appContext.Database.GetDbConnection()
            .QueryAsync<Role>(command);
        return result.ToList();
    }
}
