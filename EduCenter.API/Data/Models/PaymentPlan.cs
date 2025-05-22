using System.ComponentModel.DataAnnotations;
using EduCenter.API.Base;
using EduCenter.API.Data.Enums;

namespace EduCenter.API.Data.Models;
public class PaymentPlan : BaseEntity
{
    public decimal TotalAmount { get; set; }
    public int NumberOfPayments { get; set; }
    [MaxLength(500)]
    public string? Note { get; set; }
    public PaymentStatus Status { get; set; }
}

