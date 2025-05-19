using MediatR;
namespace EduCenter.API.Features.Users.AssignRole;
public sealed record AssignRoleCommand(int userId, int roleId) : IRequest<Unit>;
public class AssignRoleHandler : IRequestHandler<AssignRoleCommand, Unit>
{
    IUnitOfWork _uow;
    public AssignRoleHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<Unit> Handle(AssignRoleCommand request, CancellationToken ct)
    {
        _uow.users.AddUserRole(request.userId, request.roleId);
        await _uow.SaveChangesAsync(ct);
        return Unit.Value;
    }
}
