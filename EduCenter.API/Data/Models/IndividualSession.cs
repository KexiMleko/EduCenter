using EduCenter.API.Base;
using EduCenter.API.Data.Enums;

namespace EduCenter.API.Data.Models;
public class IndividualSession : BaseEntity
{

    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public int TeacherId { get; set; }
    public int StudentId { get; set; }
    public int SubjectId { get; set; }
    public int PaymentPlanId { get; set; }
    public SessionStatus Status { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public StudentPerformance performance { get; set; }
}
