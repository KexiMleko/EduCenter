using EduCenter.API.Base;

namespace EduCenter.API.Data.Models;

public class Group : BaseEntity
{
    public string Name { get; set; } = "";
    public int TeacherId { get; set; }
    public int SubjectId { get; set; }
    public bool IsActive { get; set; }
    public int MaxNumberOfClasses { get; set; }
    public int NumberOfClassesLeft { get; set; }
}

