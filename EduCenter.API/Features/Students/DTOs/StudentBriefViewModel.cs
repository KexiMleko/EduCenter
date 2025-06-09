
namespace EduCenter.API.Features.Students.DTOs;
public class StudentBriefViewModel
{
    public int Id { get; set; }
    public string LevelOfStudyTitle { get; set; } = "";
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public int AcademicYear { get; set; }
}
