using Dapper;
using EduCenter.API.Data;
using EduCenter.API.Features.Students.DTOs;
using EduCenter.API.Shared.Filters;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EduCenter.API.Features.Students.GetStudentsPaged;
public sealed record GetStudentsPagedQuery(PagedRequest<StudentFilter> request) : IRequest<PagedResult<StudentViewModel>>;
public class GetStudentsPagedHandler : IRequestHandler<GetStudentsPagedQuery, PagedResult<StudentViewModel>>
{
    private readonly DatabaseContext _appContext;
    public GetStudentsPagedHandler(DatabaseContext appContext)
    {
        _appContext = appContext;
    }
    public async Task<PagedResult<StudentViewModel>> Handle(GetStudentsPagedQuery query, CancellationToken ct)
    {
        var filters = query.request.Filters;
        var parameters = new DynamicParameters();
        var sqlFilters = LoadFilters(parameters, filters);
        var cte = @$"WITH paged AS MATERIALIZED 
                (SELECT s.id,s.level_id AS LevelOfStudyId,l.title AS LevelOfStudyTitle, s.email, s.first_name AS FirstName, s.last_name AS LastName,
                s.phone_number AS PhoneNumber, s.address, s.note,s.academic_year AS AcademicYear 
                FROM students s JOIN levels_of_study l ON s.level_id=l.id WHERE 1=1 {sqlFilters})";
        var finalQuery = $"{cte} SELECT * FROM paged LIMIT @Limit OFFSET @Offset;{cte} SELECT COUNT(*) FROM paged";

        parameters.Add("Offset", query.request.StartIndex);
        parameters.Add("Limit", query.request.Limit);

        var command = new CommandDefinition(
            finalQuery,
            parameters,
            cancellationToken: ct
        );

        var connection = _appContext.Database.GetDbConnection();
        using var multi = await connection.QueryMultipleAsync(command);
        var users = (await multi.ReadAsync<StudentViewModel>()).ToList();
        int totalCount = await multi.ReadSingleAsync<int>();
        return new PagedResult<StudentViewModel>
        {
            Limit = query.request.Limit,
            StartIndex = query.request.StartIndex,
            TotalCount = totalCount,
            Items = users.ToList(),
        };
    }
    string LoadFilters(DynamicParameters parameters, StudentFilter? filters)
    {
        var sql = "";
        if (filters == null) return sql;
        if (filters.AcademicYear != null)
        {
            sql += " AND s.academic_year = @AcademicYear";
            parameters.Add("AcademicYear", filters.AcademicYear);
        }
        if (!string.IsNullOrWhiteSpace(filters.LastName))
        {
            sql += " AND s.last_name ILIKE @LastName";
            parameters.Add("LastName", $"%{filters.LastName}%");
        }
        if (!string.IsNullOrWhiteSpace(filters.FirstName))
        {
            sql += " AND s.first_name ILIKE @FirstName";
            parameters.Add("FirstName", $"%{filters.FirstName}%");
        }
        if (filters.LevelOfStudyId != null)
        {
            sql += " AND l.id = @LevelId";
            parameters.Add("LevelId", filters.LevelOfStudyId);
        }
        return sql;
    }
}
