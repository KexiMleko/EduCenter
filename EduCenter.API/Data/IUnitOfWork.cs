using EduCenter.API.Features.Roles;
using EduCenter.API.Features.Subjects;
using EduCenter.API.Features.Users;

public interface IUnitOfWork
{
    Task SaveChangesAsync(CancellationToken ct);
    IUserRepository users { get; }
    IRoleRepository roles { get; }
    ISubjectRepository subjects { get; }
}
