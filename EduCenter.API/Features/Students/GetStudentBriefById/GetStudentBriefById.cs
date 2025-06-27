using Dapper;
using EduCenter.API.Data;
using EduCenter.API.Features.Students.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EduCenter.API.Features.Students;
public record GetStudentBriefByIdQuery(int Id) : IRequest<StudentBriefViewModel>;
public class GetStudentBriefByIdHandler : IRequestHandler<GetStudentBriefByIdQuery, StudentBriefViewModel>
{
    private readonly DatabaseContext _appContext;
    public GetStudentBriefByIdHandler(DatabaseContext appContext)
    {
        _appContext = appContext;
    }
    public async Task<StudentBriefViewModel> Handle(GetStudentBriefByIdQuery request, CancellationToken cancellationToken)
    {

        var sql = @"SELECT s.id, los.title AS levelOfStudyTitle, s.first_name AS FirstName, s.last_name AS LastName,s.academic_year AS AcademicYear 
                    FROM students s 
                    JOIN levels_of_study los ON los.id=s.level_id
                    WHERE s.id= @StudentId";

        var command = new CommandDefinition(
            sql,
            new { StudentId = request.Id },
            cancellationToken: cancellationToken
        );

        return await _appContext.Database.GetDbConnection()
            .QuerySingleOrDefaultAsync<StudentBriefViewModel>(command);
    }
}

