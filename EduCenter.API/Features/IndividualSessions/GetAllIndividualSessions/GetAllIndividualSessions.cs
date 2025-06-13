
using Dapper;
using EduCenter.API.Data;
using EduCenter.API.Data.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
namespace EduCenter.API.Features.IndividualSessions.GetAllIndividualSessions;
public sealed record GetAllIndividualSessionsQuery() : IRequest<List<IndividualSession>>;
public class GetAllIndividualSessionsHandler : IRequestHandler<GetAllIndividualSessionsQuery, List<IndividualSession>>
{
    private readonly DatabaseContext _appContext;
    public GetAllIndividualSessionsHandler(DatabaseContext appContext)
    {
        _appContext = appContext;
    }

    public async Task<List<IndividualSession>> Handle(GetAllIndividualSessionsQuery request, CancellationToken cancellationToken)
    {
        var sql = @"SELECT * FROM individual_sessions";
        var command = new CommandDefinition(
            sql,
            cancellationToken: cancellationToken
        );

        var result = await _appContext.Database.GetDbConnection()
            .QueryAsync<IndividualSession>(command);
        return result.ToList();
    }
}
