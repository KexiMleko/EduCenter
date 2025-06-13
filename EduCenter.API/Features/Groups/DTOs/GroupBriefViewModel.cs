namespace EduCenter.API.Features.Groups.DTOs;
public class GroupBriefViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public int TeacherId { get; set; }
    public string TeacherFirstName { get; set; } = "";
    public string TeacherLastName { get; set; } = "";
    public int SubjectId { get; set; }
    public string SubjectName { get; set; } = "";
}
