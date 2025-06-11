using EduCenter.API.Features.Attendances;
using EduCenter.API.Features.Enrollments;
using EduCenter.API.Features.Groups;
using EduCenter.API.Features.GroupSessions;
using EduCenter.API.Features.LevelOfStudys;
using EduCenter.API.Features.PaymentPlans;
using EduCenter.API.Features.Roles;
using EduCenter.API.Features.Students;
using EduCenter.API.Features.Subjects;
using EduCenter.API.Features.Users;

public interface IUnitOfWork
{
    Task BeginTransactionAsync(CancellationToken ct = default);
    Task CommitTransactionAsync(CancellationToken ct = default);
    Task RollbackTransactionAsync(CancellationToken ct = default);
    Task SaveChangesAsync(CancellationToken ct);

    IUserRepository users { get; }
    IRoleRepository roles { get; }
    ISubjectRepository subjects { get; }
    ILevelOfStudyRepository levelsOfStudy { get; }
    IGroupRepository groups { get; }
    IEnrollmentRepository enrollments { get; }
    IStudentRepository students { get; }
    IGroupSessionRepository groupSessions { get; }
    IAttendanceRepository attendances { get; }
    IPaymentPlanRepository paymentPlans { get; }
}
