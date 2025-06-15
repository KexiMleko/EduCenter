using EduCenter.API.Data.Enums;
using EduCenter.API.Data.Models;
using MediatR;

namespace EduCenter.API.Features.IndividualSessions.CreateIndividualSession;
public sealed record CreateIndividualSessionCommand(
    string Title,
    string? Description,
    int StudentId,
    int TeacherId,
    int SubjectId,
    DateTime TimeScheduled,
    int ClassroomId,
    SessionStatus Status,
    int SessionDuration,
    int Amount
) : IRequest<Unit>;

public class CreateIndividualSessionHandler : IRequestHandler<CreateIndividualSessionCommand, Unit>
{
    IUnitOfWork _uow;
    public CreateIndividualSessionHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<Unit> Handle(CreateIndividualSessionCommand request, CancellationToken cancellationToken)
    {
        var paymentPlan = new PaymentPlan
        {
            TotalAmount = request.Amount,
            NumberOfPayments = 1,
            Status = PaymentStatus.Pending
        };
        _uow.paymentPlans.AddPaymentPlan(paymentPlan);
        _uow.individualSessions.AddIndividualSession(new IndividualSession
        {
            Title = request.Title,
            Description = request.Description,
            StudentId = request.StudentId,
            TeacherId = request.TeacherId,
            SubjectId = request.SubjectId,
            Status = request.Status,
            TimeScheduled = request.TimeScheduled,
            ClassroomId = request.ClassroomId,
            SessionDuration = request.SessionDuration,
            PaymentPlan = paymentPlan,
            CreatedAt = DateTime.UtcNow
        });
        await _uow.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
