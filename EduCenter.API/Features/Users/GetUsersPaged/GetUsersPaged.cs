
using System.Text;
using Dapper;
using EduCenter.API.Data;
using EduCenter.API.Features.Users.DTOs;
using EduCenter.API.Shared.Filters;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EduCenter.API.Features.Users.GetUsersPaged;
public sealed record GetUsersPagedQuery(PagedRequest<UserFilter> request) : IRequest<PagedResult<UserViewModel>>;
public class GetUsersPagedHandler : IRequestHandler<GetUsersPagedQuery, PagedResult<UserViewModel>>
{
    private readonly DatabaseContext _appContext;
    public GetUsersPagedHandler(DatabaseContext appContext)
    {
        _appContext = appContext;
    }
    public async Task<PagedResult<UserViewModel>> Handle(GetUsersPagedQuery query, CancellationToken ct)
    {
        var filters = query.request.Filters;
        var cte = @"WITH paged AS MATERIALIZED 
                (SELECT id, username, email, first_name AS FirstName, last_name AS LastName, phone_number AS PhoneNumber, address, note 
                    FROM users WHERE 1=1";
        var parameters = new DynamicParameters();
        if (filters != null)
            ApplyFilters(cte, parameters, filters);
        cte += @")";
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
        var users = (await multi.ReadAsync<UserViewModel>()).ToList();
        int totalCount = await multi.ReadSingleAsync<int>();
        return new PagedResult<UserViewModel>
        {
            Limit = query.request.Limit,
            StartIndex = query.request.StartIndex,
            TotalCount = totalCount,
            Items = users.ToList(),
        };
    }
    void ApplyFilters(string sql, DynamicParameters parameters, UserFilter filters)
    {
        if (!string.IsNullOrWhiteSpace(filters.Username))
        {
            sql += " AND username ILIKE @Username";
            parameters.Add("Username", $"%{filters.Username}%");
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
