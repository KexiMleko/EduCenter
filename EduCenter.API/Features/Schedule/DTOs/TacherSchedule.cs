namespace EduCenter.API.Features.Schedules.DTOs;
public class TeacherSchedule
{
    public List<GroupSessionEvent> GroupSessions { get; set; } = new();
    public List<IndividualSessionEvent> IndividualSessions { get; set; } = new();

}
public class GroupSessionEvent
{
    public int SessionId { get; set; }
    public int GroupId { get; set; }
    public int SessionDuration { get; set; }
    public string GroupName { get; set; } = "";
    public string ClassroomName { get; set; } = "";
    public string SessionTitle { get; set; } = "";
    public DateTime StartTime { get; set; }
}
public class IndividualSessionEvent
{
    public int SessionId { get; set; }
    public int StudentId { get; set; }
    public int SessionDuration { get; set; }
    public string ClassroomName { get; set; } = "";
    public string SubjectName { get; set; } = "";
    public string SessionTitle { get; set; } = "";
    public DateTime StartTime { get; set; }
}
