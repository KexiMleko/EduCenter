namespace EduCenter.API.Features.Students.DTOs;
public class StudentViewModel
{
    public int Id { get; set; }
    public int LevelOfStudyId { get; set; }
    public string LevelOfStudyTitle { get; set; } = "";
    public string Email { get; set; } = "";
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public string PhoneNumber { get; set; } = "";
    public string Address { get; set; } = "";
    public string? Note { get; set; }
    public int AcademicYear { get; set; }
    public DateTime DateAdded { get; set; }
}
