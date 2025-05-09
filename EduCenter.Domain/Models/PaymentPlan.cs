using EduCenter.Domain.Enums;

namespace EduCenter.Domain.Models;
public class PaymentPlan : BaseEntity
{
    public int StudentId { get; set; }
    public decimal TotalAmount { get; set; }
    public int NumberOfPayments { get; set; }
    public string Note { get; set; } = "";
    public PaymentStatus Status { get; set; }
}

