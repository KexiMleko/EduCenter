using EduCenter.API.Base;
using EduCenter.API.Data.Enums;

namespace EduCenter.API.Data.Models;
public class PaymentPlan : BaseEntity
{
    public int StudentId { get; set; }
    public decimal TotalAmount { get; set; }
    public int NumberOfPayments { get; set; }
    public string Note { get; set; } = "";
    public PaymentStatus Status { get; set; }
}

