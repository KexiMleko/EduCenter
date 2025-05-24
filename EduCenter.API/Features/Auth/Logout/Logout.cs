using MediatR;

namespace EduCenter.API.Features.Auth;
public sealed record LogoutCommand(string token) : IRequest<Unit>;
public class LogoutHandler : IRequestHandler<LogoutCommand, Unit>
{
    IUnitOfWork _uow;
    public LogoutHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }
    public Task<Unit> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        _uow.users.DeleteRefreshToken(request.token);
        _uow.SaveChangesAsync(cancellationToken);
        return Task.FromResult(Unit.Value);
    }
}
