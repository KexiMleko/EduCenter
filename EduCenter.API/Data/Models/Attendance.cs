using System.ComponentModel.DataAnnotations;
using EduCenter.API.Base;
using EduCenter.API.Data.Enums;

namespace EduCenter.API.Data.Models;

public class Attendance : BaseEntity
{
    public int SessionId { get; set; }
    public int StudentId { get; set; }
    public bool WasPresent { get; set; }
    public StudentPerformance Performance { get; set; }
    [MaxLength(500)]
    public string? Note { get; set; }
}
