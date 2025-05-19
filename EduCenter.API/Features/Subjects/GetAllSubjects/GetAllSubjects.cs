using Dapper;
using EduCenter.API.Data;
using EduCenter.API.Data.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
namespace EduCenter.API.Features.Subjects.GetAllSubjects;
public sealed record GetAllSubjectsQuery() : IRequest<List<Subject>>;
public class GetAllSubjectsHandler : IRequestHandler<GetAllSubjectsQuery, List<Subject>>
{
    private readonly DatabaseContext _appContext;
    public GetAllSubjectsHandler(DatabaseContext appContext)
    {
        _appContext = appContext;
    }

    public async Task<List<Subject>> Handle(GetAllSubjectsQuery request, CancellationToken cancellationToken)
    {
        var sql = @"SELECT * FROM subjects";
        var command = new CommandDefinition(
            sql,
            cancellationToken: cancellationToken
        );

        var result = await _appContext.Database.GetDbConnection()
            .QueryAsync<Subject>(command);
        return result.ToList();
    }
}
