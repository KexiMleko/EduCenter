namespace EduCenter.Domain.Models;
public class Student : BaseEntity
{
    public int LevelId { get; set; }
    public string Email { get; set; } = "";
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public string PhoneNumber { get; set; } = "";
    public string Address { get; set; } = "";
    public string Note { get; set; } = "";
    public int AcademicYear { get; set; }
}
