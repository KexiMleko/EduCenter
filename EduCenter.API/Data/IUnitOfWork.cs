using EduCenter.API.Features.Users;

public interface IUnitOfWork
{
    Task SaveChangesAsync(CancellationToken ct);
    IUserRepository users { get; }
}
