using EduCenter.Domain.Enums;

namespace EduCenter.Domain.Models;

public class Attendance : BaseEntity
{
    public int SessionId { get; set; }
    public int StudentId { get; set; }
    public bool WasPresent { get; set; }
    public StudentPerformance performance { get; set; }
    public string Note { get; set; } = "";
}
