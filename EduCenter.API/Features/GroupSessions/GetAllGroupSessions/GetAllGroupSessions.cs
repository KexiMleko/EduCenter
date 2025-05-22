
using Dapper;
using EduCenter.API.Data;
using EduCenter.API.Data.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
namespace EduCenter.API.Features.GroupSessions.GetAllGroupSessions;
public sealed record GetAllGroupSessionsQuery() : IRequest<List<GroupSession>>;
public class GetAllGroupSessionsHandler : IRequestHandler<GetAllGroupSessionsQuery, List<GroupSession>>
{
    private readonly DatabaseContext _appContext;
    public GetAllGroupSessionsHandler(DatabaseContext appContext)
    {
        _appContext = appContext;
    }

    public async Task<List<GroupSession>> Handle(GetAllGroupSessionsQuery request, CancellationToken cancellationToken)
    {
        var sql = @"SELECT * FROM groupSessions";
        var command = new CommandDefinition(
            sql,
            cancellationToken: cancellationToken
        );

        var result = await _appContext.Database.GetDbConnection()
            .QueryAsync<GroupSession>(command);
        return result.ToList();
    }
}
