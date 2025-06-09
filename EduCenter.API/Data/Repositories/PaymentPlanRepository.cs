using EduCenter.API.Data;
using EduCenter.API.Data.Models;
using EduCenter.API.Features.PaymentPlans;

namespace EduCenter.API.Data.Repositories;
public class PaymentPlanRepository : IPaymentPlanRepository
{
    private readonly DatabaseContext _appContext;
    public PaymentPlanRepository(DatabaseContext appContext)
    {
        _appContext = appContext;
    }

    public void AddPaymentPlan(PaymentPlan paymentPlan)
    {
        _appContext.PaymentPlans.Add(paymentPlan);
    }

    public void UpdatePaymentPlan(PaymentPlan paymentPlan)
    {
        _appContext.PaymentPlans.Update(paymentPlan);
    }
}
