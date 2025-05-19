using EduCenter.API.Data.Models;
using EduCenter.API.Shared.Services.Hashing;
using EduCenter.API.Shared.Services.JWT;
using MediatR;

namespace EduCenter.API.Features.Auth;
public sealed record LoginCommand(string username, string password) : IRequest<Unit>;
public class LoginHandler : IRequestHandler<LoginCommand, Unit>
{
    private readonly IUnitOfWork _uow;
    private readonly IPasswordHashService _hasher;
    private readonly IJwtHelper _jwtHelper;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IConfiguration _configuration;

    public LoginHandler(IUnitOfWork uow, IPasswordHashService hasher, IJwtHelper jwtHelper, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
    {
        _uow = uow;
        _hasher = hasher;
        _jwtHelper = jwtHelper;
        _httpContextAccessor = httpContextAccessor;
        _configuration = configuration;
    }
    public async Task<Unit> Handle(LoginCommand request, CancellationToken ct)
    {
        User user = await _uow.users.GetUserByUsername(request.username, ct);
        _hasher.VerifyPassword(request.password, user.PasswordHash);
        if (user == null || !_hasher.VerifyPassword(request.password, user.PasswordHash))
            throw new UnauthorizedAccessException("Invalid username or password");
        var accessToken = _jwtHelper.GenerateAccessToken(user);
        var refreshToken = new RefreshToken
        {
            UserId = user.Id,
            ExpiresAt = DateTime.UtcNow.AddDays(Int32.Parse(_configuration["Jwt:RefreshTokenExpiry"] ?? "7")),
            Token = _jwtHelper.GenerateRefreshToken()
        };
        await _uow.users.UpdateRefreshToken(refreshToken, ct);
        var response = _httpContextAccessor.HttpContext?.Response;
        if (response != null)
        {
            response.Cookies.Append("AccessToken", accessToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddMinutes(Int32.Parse(_configuration["Jwt:AccessTokenExpiry"] ?? "7"))
            });
            response.Cookies.Append("RefreshToken", refreshToken.Token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = refreshToken.ExpiresAt
            });
        }
        return Unit.Value;
    }
}
