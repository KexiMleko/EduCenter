using EduCenter.API.Data.Enums;
using EduCenter.API.Data.Models;
using MediatR;

namespace EduCenter.API.Features.PaymentPlans.CreatePaymentPlan;
public sealed record CreatePaymentPlanCommand(int totalAmount, int numberOfPayments, string? note, PaymentStatus status) : IRequest<Unit>;
public class CreatePaymentPlanHandler : IRequestHandler<CreatePaymentPlanCommand, Unit>
{
    IUnitOfWork _uow;
    public CreatePaymentPlanHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<Unit> Handle(CreatePaymentPlanCommand request, CancellationToken cancellationToken)
    {
        _uow.paymentPlans.AddPaymentPlan(new PaymentPlan
        {
            TotalAmount = request.totalAmount,
            NumberOfPayments = request.numberOfPayments,
            Note = request.note,
            Status = request.status,
            CreatedAt = DateTime.UtcNow
        });
        await _uow.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
