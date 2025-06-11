using EduCenter.API.Base;

namespace EduCenter.API.Data.Models;

public class Enrollment : BaseEntity
{
    public int GroupId { get; set; }
    public int StudentId { get; set; }
    public PaymentPlan PaymentPlan { get; set; }
    public int PaymentPlanId { get; set; }
}

