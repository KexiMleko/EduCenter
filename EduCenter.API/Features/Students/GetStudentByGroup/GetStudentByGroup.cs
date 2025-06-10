using Dapper;
using EduCenter.API.Data;
using EduCenter.API.Features.Students.DTOs;
using EduCenter.API.Features.Users.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EduCenter.API.Features.Users.GetStudentByGroup;
public sealed record GetStudentByGroupQuery(int groupId) : IRequest<List<StudentBriefViewModel>>;
public class GetStudentByGroupHandler : IRequestHandler<GetStudentByGroupQuery, List<StudentBriefViewModel>>
{
    private readonly DatabaseContext _appContext;
    public GetStudentByGroupHandler(DatabaseContext appContext)
    {
        _appContext = appContext;
    }
    public async Task<List<StudentBriefViewModel>> Handle(GetStudentByGroupQuery request, CancellationToken ct)
    {
        var sql = @"SELECT s.id, los.title AS levelOfStudyTitle, s.first_name AS FirstName, s.last_name AS LastName,s.academic_year AS AcademicYear 
                    FROM enrollments e 
                    JOIN students s ON e.student_id=s.id
                    JOIN levels_of_study los ON los.id=s.level_id
                    WHERE e.group_id = @GroupId";

        var command = new CommandDefinition(
            sql,
            new { GroupId = request.groupId },
            cancellationToken: ct
        );

        List<StudentBriefViewModel> result = (await _appContext.Database.GetDbConnection()
            .QueryAsync<StudentBriefViewModel>(command)).ToList();
        return result;
    }
}
