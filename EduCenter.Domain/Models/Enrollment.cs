namespace EduCenter.Domain.Models;

public class Enrollment : BaseEntity
{
    public int GroupId { get; set; }
    public int StudentId { get; set; }
    public int PaymentPlanId { get; set; }
}

