namespace EduCenter.Domain.Models;
public class Payment : BaseEntity
{
    public int PaymentPlanId { get; set; }
    public decimal Amount { get; set; }
}
