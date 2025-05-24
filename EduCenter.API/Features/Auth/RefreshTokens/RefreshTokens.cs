using EduCenter.API.Data.Models;
using EduCenter.API.Shared.Services.JWT;
using MediatR;
namespace EduCenter.API.Features.Auth.RefreshTokens;
public sealed record RefreshTokensCommand(string token) : IRequest<Unit>;
public class RefreshTokensHandler : IRequestHandler<RefreshTokensCommand, Unit>
{
    private readonly IUnitOfWork _uow;
    private readonly IJwtHelper _jwtHelper;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IConfiguration _configuration;


    public RefreshTokensHandler(IUnitOfWork uow, IJwtHelper jwtHelper, IHttpContextAccessor httpContext, IConfiguration configuration)
    {
        _uow = uow;
        _jwtHelper = jwtHelper;
        _httpContextAccessor = httpContext;
        _configuration = configuration;
    }
    public async Task<Unit> Handle(RefreshTokensCommand request, CancellationToken cancellationToken)
    {
        var user = await _uow.users.GetUserByRefreshToken(request.token);
        var accessToken = _jwtHelper.GenerateAccessToken(user);
        var refreshToken = new RefreshToken
        {
            UserId = user.Id,
            ExpiresAt = DateTime.UtcNow.AddDays(Int32.Parse(_configuration["Jwt:RefreshTokenExpiry"] ?? "7")),
            Token = _jwtHelper.GenerateRefreshToken()
        };
        _uow.users.UpdateRefreshToken(refreshToken);
        var response = _httpContextAccessor.HttpContext?.Response;
        if (response != null)
        {
            response.Cookies.Append("AccessToken", accessToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddMinutes(Int32.Parse(_configuration["Jwt:AccessTokenExpiry"] ?? "15"))
            });
            response.Cookies.Append("RefreshToken", refreshToken.Token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = refreshToken.ExpiresAt
            });
        }
        await _uow.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
