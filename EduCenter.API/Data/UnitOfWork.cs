
using EduCenter.API.Data.Repositories;
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

namespace EduCenter.API.Data;
public class UnitOfWork : IUnitOfWork
{
    private readonly DatabaseContext _appContext;
    public UnitOfWork(DatabaseContext appContext)
    {
        _appContext = appContext;
    }
    IUserRepository _users;
    IRoleRepository _roles;
    ISubjectRepository _subjects;
    ILevelOfStudyRepository _levelsOfStudy;
    IGroupRepository _groups;
    IStudentRepository _students;
    IEnrollmentRepository _enrollments;
    IGroupSessionRepository _groupSessions;
    IAttendanceRepository _attendances;
    IPaymentPlanRepository _paymentPlans;
    // NOTE: Getters
    public IUserRepository users => _users ??= new UserRepository(_appContext);
    public IPaymentPlanRepository paymentPlans => _paymentPlans ??= new PaymentPlanRepository(_appContext);
    public IStudentRepository students => _students ??= new StudentRepository(_appContext);
    public IRoleRepository roles => _roles ??= new RoleRepository(_appContext);
    public IGroupRepository groups => _groups ??= new GroupRepository(_appContext);
    public IGroupSessionRepository groupSessions => _groupSessions ??= new GroupSessionRepository(_appContext);
    public ISubjectRepository subjects => _subjects ??= new SubjectRepository(_appContext);
    public ILevelOfStudyRepository levelsOfStudy => _levelsOfStudy ??= new LevelOfStudyRepository(_appContext);
    public IEnrollmentRepository enrollments => _enrollments ??= new EnrollmentRepository(_appContext);
    public IAttendanceRepository attendances => _attendances ??= new AttendanceRepository(_appContext);

    // NOTE: Save changes and transaction managment
    public async Task SaveChangesAsync(CancellationToken ct)
    {
        await _appContext.SaveChangesAsync(ct);
    }
    public async Task BeginTransactionAsync(CancellationToken ct = default)
    {
        await _appContext.Database.BeginTransactionAsync(ct);
    }

    public async Task CommitTransactionAsync(CancellationToken ct = default)
    {
        await _appContext.Database.CommitTransactionAsync(ct);
    }

    public async Task RollbackTransactionAsync(CancellationToken ct = default)
    {
        await _appContext.Database.RollbackTransactionAsync(ct);
    }
}
