using System.ComponentModel.DataAnnotations;
using EduCenter.API.Base;
using EduCenter.API.Data.Enums;

namespace EduCenter.API.Data.Models;
public class IndividualSession : BaseEntity
{
    [MaxLength(100)]
    public string Title { get; set; } = "";
    [MaxLength(500)]
    public string? Description { get; set; }
    public int TeacherId { get; set; }
    public int StudentId { get; set; }
    public int SubjectId { get; set; }
    public int ClassroomId { get; set; }
    public int PaymentPlanId { get; set; }
    public DateTime TimeScheduled { get; set; }
    public SessionStatus Status { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public StudentPerformance performance { get; set; }
}
