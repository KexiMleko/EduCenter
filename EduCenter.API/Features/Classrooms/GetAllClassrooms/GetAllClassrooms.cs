using Dapper;
using EduCenter.API.Data;
using EduCenter.API.Data.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
namespace EduCenter.API.Features.Classrooms.GetAllClassrooms;
public sealed record GetAllClassroomsQuery() : IRequest<List<Classroom>>;
public class GetAllClassroomsHandler : IRequestHandler<GetAllClassroomsQuery, List<Classroom>>
{
    private readonly DatabaseContext _appContext;
    public GetAllClassroomsHandler(DatabaseContext appContext)
    {
        _appContext = appContext;
    }

    public async Task<List<Classroom>> Handle(GetAllClassroomsQuery request, CancellationToken cancellationToken)
    {
        var sql = @"SELECT * FROM classrooms";
        var command = new CommandDefinition(
            sql,
            cancellationToken: cancellationToken
        );

        var result = await _appContext.Database.GetDbConnection()
            .QueryAsync<Classroom>(command);
        return result.ToList();
    }
}
