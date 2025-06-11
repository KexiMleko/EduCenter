using EduCenter.API.Data.Enums;

public class StudentEnrollmentDto
{
    public int StudentId { get; set; }
    public int GroupId { get; set; }
    public PaymentPlanAddDto PaymentPlan { get; set; }
}
public class PaymentPlanAddDto
{
    public int totalAmount { get; set; }
    public int numberOfPayments { get; set; }
    public string? note { get; set; }
    public PaymentStatus status { get; set; }
}
