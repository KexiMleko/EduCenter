using Dapper;
using EduCenter.API.Data;
using EduCenter.API.Features.Groups.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EduCenter.API.Features.Groups.GetAllGroupsBrief;

public sealed record GetAllGroupsBriefQuery() : IRequest<List<GroupBriefViewModel>>;
public class GetAllGroupsBriefHandler : IRequestHandler<GetAllGroupsBriefQuery, List<GroupBriefViewModel>>
{
    private readonly DatabaseContext _appContext;
    public GetAllGroupsBriefHandler(DatabaseContext appContext)
    {
        _appContext = appContext;
    }

    public async Task<List<GroupBriefViewModel>> Handle(GetAllGroupsBriefQuery request, CancellationToken cancellationToken)
    {
        var sql = @"SELECT 
        g.id,
        g.name,
        g.teacher_id AS TeacherId,
        t.first_name AS TeacherFirstName,
        t.last_name AS TeacherLastName,
        g.subject_id AS SubjectId,
        s.name AS SubjectName 
    FROM groups g 
    JOIN users t ON g.teacher_id = t.id
    JOIN subjects s ON g.subject_id = s.id";
        var command = new CommandDefinition(
            sql,
            cancellationToken: cancellationToken
        );

        var result = await _appContext.Database.GetDbConnection()
            .QueryAsync<GroupBriefViewModel>(command);
        return result.ToList();
    }
}
