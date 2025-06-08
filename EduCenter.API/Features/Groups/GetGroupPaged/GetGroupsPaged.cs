using Dapper;
using EduCenter.API.Data;
using EduCenter.API.Features.Groups.DTOs;
using EduCenter.API.Shared.Filters;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EduCenter.API.Features.Groups.GetGroupsPaged;
public sealed record GetGroupsPagedQuery(PagedRequest<GroupFilter> request) : IRequest<PagedResult<GroupViewModel>>;
public class GetGroupsPagedHandler : IRequestHandler<GetGroupsPagedQuery, PagedResult<GroupViewModel>>
{
    private readonly DatabaseContext _appContext;
    public GetGroupsPagedHandler(DatabaseContext appContext)
    {
        _appContext = appContext;
    }
    public async Task<PagedResult<GroupViewModel>> Handle(GetGroupsPagedQuery query, CancellationToken ct)
    {
        var filters = query.request.Filters;

        var cte = @"
WITH student_counts AS (
    SELECT group_id, COUNT(*) AS number_of_students
    FROM enrollments
    GROUP BY group_id
),
paged AS MATERIALIZED (
    SELECT 
        g.id,
        g.name,
        g.teacher_id AS TeacherId,
        t.first_name AS TeacherFirstName,
        t.last_name AS TeacherLastName,
        g.subject_id AS SubjectId,
        s.name AS SubjectName,
        g.is_active AS IsActive,
        g.max_number_of_classes AS MaxNumberOfClasses,
        g.number_of_classes_left AS NumberOfClassesLeft,
        g.created_at AS CreatedAt,
        g.updated_at AS UpdatedAt,
        coalesce (sc.number_of_students,0) AS StudentCount
        
    FROM groups g 
    JOIN users t ON g.teacher_id = t.id
    JOIN subjects s ON g.subject_id = s.id
    LEFT JOIN student_counts sc ON sc.group_id = g.id
                WHERE 1=1";
        var parameters = new DynamicParameters();
        if (filters != null)
            ApplyFilters(cte, parameters, filters);
        cte += @" )";
        var finalQuery = $" {cte} SELECT * FROM paged LIMIT @Limit OFFSET @Offset;{cte} SELECT COUNT(*) FROM paged";

        parameters.Add("Offset", query.request.StartIndex);
        parameters.Add("Limit", query.request.Limit);

        var command = new CommandDefinition(
            finalQuery,
            parameters,
            cancellationToken: ct
        );

        var connection = _appContext.Database.GetDbConnection();
        using var multi = await connection.QueryMultipleAsync(command);
        var groups = (await multi.ReadAsync<GroupViewModel>()).ToList();
        int totalCount = await multi.ReadSingleAsync<int>();
        return new PagedResult<GroupViewModel>
        {
            Limit = query.request.Limit,
            StartIndex = query.request.StartIndex,
            TotalCount = totalCount,
            Items = groups.ToList(),
        };
    }
    void ApplyFilters(string sql, DynamicParameters parameters, GroupFilter filters)
    {
        if (filters.Name != null)
        {
            sql += " AND name = @Name";
            parameters.Add("Name", $"{filters.Name}");
        }
        if (filters.TeacherId != null)
        {
            sql += " AND teacher_id = @TeacherId";
            parameters.Add("TeacherId", filters.TeacherId);
        }
        if (filters.MaxNumberOfClassesRange != null)
        {
            sql += " AND max_number_of_classes > @NocMin AND max_number_of_classes < @NocMax";
            parameters.Add("NocMax", filters.MaxNumberOfClassesRange.Max);
            parameters.Add("NocMin", filters.MaxNumberOfClassesRange.Min);
        }
    }
}
