namespace EduCenter.Domain.Models;

public class Group : BaseEntity
{
    public int TeacherId { get; set; }
    public int SubjectId { get; set; }
    public bool IsActive { get; set; }
    public int MaxNumberOfClasses { get; set; }
    public int NumberOfClassesLeft { get; set; }
}

