
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
    DatabaseContext _appContext;
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
    public IUserRepository users
    {
        get => _users != null ? _users : new UserRepository(_appContext);
    }
    public IPaymentPlanRepository paymentPlans
    {
        get => _paymentPlans != null ? _paymentPlans : new PaymentPlanRepository(_appContext);
    }
    public IStudentRepository students
    {
        get => _students != null ? _students : new StudentRepository(_appContext);
    }
    public IRoleRepository roles
    {
        get => _roles != null ? _roles : new RoleRepository(_appContext);
    }
    public IGroupRepository groups
    {
        get => _groups != null ? _groups : new GroupRepository(_appContext);
    }
    public IGroupSessionRepository groupSessions
    {
        get => _groupSessions != null ? _groupSessions : new GroupSessionRepository(_appContext);
    }
    public ISubjectRepository subjects
    {
        get => _subjects != null ? _subjects : new SubjectRepository(_appContext);
    }
    public ILevelOfStudyRepository levelsOfStudy
    {
        get => _levelsOfStudy != null ? _levelsOfStudy : new LevelOfStudyRepository(_appContext);
    }

    public IEnrollmentRepository enrollments
    {
        get => _enrollments != null ? _enrollments : new EnrollmentRepository(_appContext);
    }
    public IAttendanceRepository attendances { get => _attendances != null ? _attendances : new AttendanceRepository(_appContext); }

    // NOTE: Save changes
    public async Task SaveChangesAsync(CancellationToken ct)
    {
        await _appContext.SaveChangesAsync(ct);
    }
}
