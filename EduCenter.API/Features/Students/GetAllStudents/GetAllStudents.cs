
using Dapper;
using EduCenter.API.Data;
using EduCenter.API.Data.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
namespace EduCenter.API.Features.Students.GetAllStudents;
public sealed record GetAllStudentsQuery() : IRequest<List<Student>>;
public class GetAllStudentsHandler : IRequestHandler<GetAllStudentsQuery, List<Student>>
{
    private readonly DatabaseContext _appContext;
    public GetAllStudentsHandler(DatabaseContext appContext)
    {
        _appContext = appContext;
    }

    public async Task<List<Student>> Handle(GetAllStudentsQuery request, CancellationToken cancellationToken)
    {
        var sql = @"SELECT * FROM studens";
        var command = new CommandDefinition(
            sql,
            cancellationToken: cancellationToken
        );

        var result = await _appContext.Database.GetDbConnection()
            .QueryAsync<Student>(command);
        return result.ToList();
    }
}
