using MediatR;

namespace EduCenter.API.Features.Auth;
public sealed record LogoutCommand(string? token) : IRequest<Unit>;
public class LogoutHandler : IRequestHandler<LogoutCommand, Unit>
{
    IUnitOfWork _uow;
    IHttpContextAccessor _httpContext;
    public LogoutHandler(IUnitOfWork uow, IHttpContextAccessor httpContext)
    {
        _httpContext = httpContext;
        _uow = uow;
    }
    public async Task<Unit> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        if (!String.IsNullOrEmpty(request.token))
            _uow.users.DeleteRefreshToken(request.token);
        var response = _httpContext.HttpContext?.Response;
        if (response != null)
        {
            response.Cookies.Append("AccessToken", "", new CookieOptions
            {
                Expires = DateTimeOffset.UtcNow.AddDays(-1),
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None
            });

            response.Cookies.Append("RefreshToken", "", new CookieOptions
            {
                Expires = DateTimeOffset.UtcNow.AddDays(-1),
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None
            });
        }
        await _uow.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
