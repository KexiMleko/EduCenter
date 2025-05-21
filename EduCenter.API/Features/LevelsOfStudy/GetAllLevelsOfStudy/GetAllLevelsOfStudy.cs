using Dapper;
using EduCenter.API.Data;
using EduCenter.API.Data.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
namespace EduCenter.API.Features.LevelsOfStudy.GetAllLevelsOfStudy;
public sealed record GetAllLevelsOfStudyQuery() : IRequest<List<LevelOfStudy>>;
public class GetAllLevelOfStudysHandler : IRequestHandler<GetAllLevelsOfStudyQuery, List<LevelOfStudy>>
{
    private readonly DatabaseContext _appContext;
    public GetAllLevelOfStudysHandler(DatabaseContext appContext)
    {
        _appContext = appContext;
    }

    public async Task<List<LevelOfStudy>> Handle(GetAllLevelsOfStudyQuery request, CancellationToken cancellationToken)
    {
        var sql = @"SELECT * FROM levels_of_study";
        var command = new CommandDefinition(
            sql,
            cancellationToken: cancellationToken
        );

        var result = await _appContext.Database.GetDbConnection()
            .QueryAsync<LevelOfStudy>(command);
        return result.ToList();
    }
}
