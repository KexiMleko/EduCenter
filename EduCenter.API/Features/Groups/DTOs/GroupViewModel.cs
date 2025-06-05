
namespace EduCenter.API.Features.Groups.DTOs;
public class GroupViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public int TeacherId { get; set; }
    public string TeacherFirstName { get; set; } = "";
    public string TeacherLastName { get; set; } = "";
    public int SubjectId { get; set; }
    public string SubjectName { get; set; } = "";
    public bool IsActive { get; set; }
    public int MaxNumberOfClasses { get; set; }
    public int NumberOfClassesLeft { get; set; }
    public List<int> StudentIds { get; set; } = new List<int>();
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
