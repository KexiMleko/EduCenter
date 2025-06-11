using EduCenter.API.Data.Models;
using MediatR;

public record EnrollStudentsWithPaymentPlansCommand(List<StudentEnrollmentDto> enrollments) : IRequest<Unit>;
public class EnrollStudentsWithPaymentPlansHandler : IRequestHandler<EnrollStudentsWithPaymentPlansCommand, Unit>
{
    private readonly IUnitOfWork _uow;

    public EnrollStudentsWithPaymentPlansHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<Unit> Handle(EnrollStudentsWithPaymentPlansCommand request, CancellationToken cancellationToken)
    {
        var enrollmentData = request.enrollments;
        try
        {
            await _uow.BeginTransactionAsync(cancellationToken);
            foreach (var enrollment in enrollmentData)
            {
                var plan = enrollment.PaymentPlan;
                var paymentPlan = new PaymentPlan
                {
                    TotalAmount = plan.totalAmount,
                    NumberOfPayments = plan.numberOfPayments,
                    Note = plan.note,
                    Status = plan.status,
                    CreatedAt = DateTime.UtcNow
                };
                _uow.paymentPlans.AddPaymentPlan(paymentPlan);
                _uow.enrollments.AddEnrollment(new Enrollment
                {
                    StudentId = enrollment.StudentId,
                    PaymentPlan = paymentPlan,
                    GroupId = enrollment.GroupId,
                    CreatedAt = DateTime.UtcNow
                });
            }
            await _uow.SaveChangesAsync(cancellationToken);
            await _uow.CommitTransactionAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            await _uow.RollbackTransactionAsync(cancellationToken);
            throw;
        }

        return Unit.Value;
    }
}
