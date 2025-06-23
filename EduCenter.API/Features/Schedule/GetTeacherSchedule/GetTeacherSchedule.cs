using Dapper;
using EduCenter.API.Data;
using EduCenter.API.Features.Schedules.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EduCenter.API.Features.Schedules;
public record GetTeacherScheduleQuery(int teacherId) : IRequest<TeacherSchedule>;
public class GetTeacherScheduleHandler : IRequestHandler<GetTeacherScheduleQuery, TeacherSchedule>
{
    private readonly DatabaseContext _appContext;
    public GetTeacherScheduleHandler(DatabaseContext appContext)
    {
        _appContext = appContext;
    }
    public async Task<TeacherSchedule> Handle(GetTeacherScheduleQuery request, CancellationToken cancellationToken)
    {
        var individualSessions = await GetIndividualSessionSchedule(request.teacherId, cancellationToken);
        var groupSessions = await GetGroupSessionSchedule(request.teacherId, cancellationToken);
        return new TeacherSchedule
        {
            IndividualSessions = individualSessions,
            GroupSessions = groupSessions
        };
    }
    private async Task<List<GroupSessionEvent>> GetGroupSessionSchedule(int teacherId, CancellationToken cancellationToken)
    {

        var sql = @"
        SELECT 
            gs.id AS SessionId,
            gs.group_id AS GroupId,
            g.name AS GroupName,
            c.name AS ClassroomName,
            gs.title AS SessionTitle,
            gs.session_duration AS SessionDuration,
            gs.time_scheduled AS StartTime
        FROM group_sessions gs
        JOIN groups g ON gs.group_id = g.id
        JOIN classrooms c ON gs.classroom_id = c.id
        WHERE gs.teacher_id = @TeacherId";

        var command = new CommandDefinition(
            sql,
            new { TeacherId = teacherId },
            cancellationToken: cancellationToken
        );

        List<GroupSessionEvent> result = (await _appContext.Database.GetDbConnection()
            .QueryAsync<GroupSessionEvent>(command)).ToList();
        return result;
    }
    private async Task<List<IndividualSessionEvent>> GetIndividualSessionSchedule(int teacherId, CancellationToken cancellationToken)
    {

        var sql = @"
        SELECT 
            s.id AS SessionId,
            s.student_id AS StudentId,
            s.session_duration AS SessionDuration,
            c.name AS ClassroomName,
            subj.name AS SubjectName,
            s.title AS SessionTitle,
            s.time_scheduled AS StartTime
        FROM individual_sessions s
        JOIN classrooms c ON s.classroom_id = c.id
        JOIN subjects subj ON s.subject_id = subj.id
        WHERE s.teacher_id = @TeacherId";
        var command = new CommandDefinition(
            sql,
            new { TeacherId = teacherId },
            cancellationToken: cancellationToken
        );

        List<IndividualSessionEvent> result = (await _appContext.Database.GetDbConnection()
            .QueryAsync<IndividualSessionEvent>(command)).ToList();
        return result;
    }
}
