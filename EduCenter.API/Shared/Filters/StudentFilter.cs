
namespace EduCenter.API.Shared.Filters;
public class StudentFilter
{
    public string? LastName { get; set; }
    public string? FirstName { get; set; }
    public int? AcademicYear { get; set; }
    public int? LevelOfStudyId { get; set; }
}
