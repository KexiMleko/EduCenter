namespace EduCenter.Domain.Models;
public class TeacherSubject : BaseEntity
{
    public int TeacherId { get; set; }
    public int SubjectId { get; set; }
    public int LevelId { get; set; }
}
