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
        var cte = @"WITH paged AS MATERIALIZED 
                (SELECT s.id,s.level_id AS LevelOfStudyId,l.title AS LevelOfStudyTitle, s.email, s.first_name AS FirstName, s.last_name AS LastName,
                s.phone_number AS PhoneNumber, s.address, s.note,s.academic_year AS AcademicYear 
                FROM students u JOIN level_of_study l ON s.level_id=l.id WHERE 1=1";
        var parameters = new DynamicParameters();
        if (filters != null)
            ApplyFilters(cte, parameters, filters);
        cte += ")";
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
    void ApplyFilters(string sql, DynamicParameters parameters, StudentFilter filters)
    {
        if (filters.AcademicYear != null)
        {
            sql += " AND academic_year = @AcademicYear";
            parameters.Add("AcademicYear", $"{filters.AcademicYear}");
        }
        if (!string.IsNullOrWhiteSpace(filters.LastName))
        {
            sql += " AND last_name ILIKE @LastName";
            parameters.Add("LastName", $"%{filters.LastName}%");
        }
        if (!string.IsNullOrWhiteSpace(filters.FirstName))
        {
            sql += " AND first_name ILIKE @FirstName";
            parameters.Add("FirstName", $"%{filters.FirstName}%");
        }
    }
}
