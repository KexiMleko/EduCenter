namespace EduCenter.API.Shared.Filters;
public class GroupFilter
{
    public string? Name { get; set; }
    public bool IsActive { get; set; }
    public int? TeacherId { get; set; }
    public IntRange? MaxNumberOfClassesRange { get; set; }
    public IntRange? NumberOfClassesLeftRange { get; set; }
}
