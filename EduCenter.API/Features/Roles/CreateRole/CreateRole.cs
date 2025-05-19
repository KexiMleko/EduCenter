using EduCenter.API.Data.Models;
using MediatR;

namespace EduCenter.API.Features.Roles.CreateRole;
public sealed record CreateRoleCommand(string name) : IRequest<Unit>;
public class CreateRoleHandler : IRequestHandler<CreateRoleCommand, Unit>
{
    IUnitOfWork _uow;
    public CreateRoleHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<Unit> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        _uow.roles.AddRole(new Role
        {
            Name = request.name,
            CreatedAt = DateTime.UtcNow
        });
        await _uow.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
