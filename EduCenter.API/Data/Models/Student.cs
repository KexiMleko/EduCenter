using System.ComponentModel.DataAnnotations;
using EduCenter.API.Base;

namespace EduCenter.API.Data.Models;
public class Student : BaseEntity
{
    public int LevelId { get; set; }
    [MaxLength(254)]
    public string? Email { get; set; }
    [MaxLength(50)]
    public string FirstName { get; set; } = "";
    [MaxLength(50)]
    public string ParentFirstName { get; set; } = "";
    [MaxLength(100)]
    public string LastName { get; set; } = "";
    [MaxLength(15)]
    public string PhoneNumber { get; set; } = "";
    [MaxLength(200)]
    public string Address { get; set; } = "";
    [MaxLength(500)]
    public string? Note { get; set; }
    public int AcademicYear { get; set; }
}
