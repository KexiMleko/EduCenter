
using EduCenter.API.Data.Models;
using MediatR;

namespace EduCenter.API.Features.Roles.UpdateRole;
public sealed record UpdateRoleCommand(int roleId, string name) : IRequest<Unit>;
public class UpdateRoleHandler : IRequestHandler<UpdateRoleCommand, Unit>
{
    IUnitOfWork _uow;
    public UpdateRoleHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<Unit> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        _uow.roles.UpdateRole(new Role
        {
            Id = request.roleId,
            Name = request.name,
            UpdatedAt = DateTime.UtcNow,
        });
        await _uow.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
