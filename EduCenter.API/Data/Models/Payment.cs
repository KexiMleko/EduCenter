using EduCenter.API.Base;

namespace EduCenter.API.Data.Models;
public class Payment : BaseEntity
{
    public int PaymentPlanId { get; set; }
    public decimal Amount { get; set; }
}
