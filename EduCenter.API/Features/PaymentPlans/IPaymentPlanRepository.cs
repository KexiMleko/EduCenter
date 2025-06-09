using EduCenter.API.Data.Models;

namespace EduCenter.API.Features.PaymentPlans;
public interface IPaymentPlanRepository
{
    void AddPaymentPlan(PaymentPlan paymentPlan);
    void UpdatePaymentPlan(PaymentPlan paymentPlan);
}
