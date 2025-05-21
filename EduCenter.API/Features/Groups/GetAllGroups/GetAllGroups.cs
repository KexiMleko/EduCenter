using Dapper;
using EduCenter.API.Data;
using EduCenter.API.Data.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
namespace EduCenter.API.Features.Groups.GetAllGroups;
public sealed record GetAllGroupsQuery() : IRequest<List<Group>>;
public class GetAllGroupsHandler : IRequestHandler<GetAllGroupsQuery, List<Group>>
{
    private readonly DatabaseContext _appContext;
    public GetAllGroupsHandler(DatabaseContext appContext)
    {
        _appContext = appContext;
    }

    public async Task<List<Group>> Handle(GetAllGroupsQuery request, CancellationToken cancellationToken)
    {
        var sql = @"SELECT * FROM groups";
        var command = new CommandDefinition(
            sql,
            cancellationToken: cancellationToken
        );

        var result = await _appContext.Database.GetDbConnection()
            .QueryAsync<Group>(command);
        return result.ToList();
    }
}
