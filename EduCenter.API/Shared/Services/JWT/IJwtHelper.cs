using EduCenter.API.Data.Models;

namespace EduCenter.API.Shared.Services.JWT;
public interface IJwtHelper
{
    string GenerateAccessToken(User user);
    string GenerateRefreshToken();
}
