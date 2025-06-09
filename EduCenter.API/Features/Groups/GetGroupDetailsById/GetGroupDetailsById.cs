using Dapper;
using EduCenter.API.Data;
using EduCenter.API.Data.Models;
using EduCenter.API.Features.Groups.DTOs;
using EduCenter.API.Features.Students.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
namespace EduCenter.API.Features.Groups.GetGroupById;
public sealed record GetGroupDetailsByIdQuery(int id) : IRequest<GroupDetailsViewModel>;
public class GetGroupDetailsByIdHandler : IRequestHandler<GetGroupDetailsByIdQuery, GroupDetailsViewModel>
{
    private readonly DatabaseContext _appContext;
    public GetGroupDetailsByIdHandler(DatabaseContext appContext)
    {
        _appContext = appContext;
    }

    public async Task<GroupDetailsViewModel> Handle(GetGroupDetailsByIdQuery request, CancellationToken cancellationToken)
    {
        var sql = @"select stu.id AS Id,
    stu.first_name AS FirstName,
    stu.last_name AS LastName,
    stu.academic_year AS AcademicYear,
    los.title AS LevelOfStudyTitle
    from enrollments e
LEFT JOIN students stu ON e.student_id = stu.id
LEFT JOIN levels_of_study los ON stu.level_id = los.id
where e.group_id=@GroupId;

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
    g.updated_at AS UpdatedAt
FROM groups g
JOIN users t ON g.teacher_id = t.id
JOIN subjects s ON g.subject_id = s.id
WHERE g.id = @GroupId;";
        var command = new CommandDefinition(
            sql,
            new { GroupId = request.id },
            cancellationToken: cancellationToken
        );

        var connection = _appContext.Database.GetDbConnection();
        using var multi = await connection.QueryMultipleAsync(command);
        List<StudentBriefViewModel> students = (await multi.ReadAsync<StudentBriefViewModel>()).ToList();
        var group = await multi.ReadSingleAsync<GroupDetailsViewModel>();
        group.Students = students;
        return group;
    }
}
