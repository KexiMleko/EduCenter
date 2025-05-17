namespace EduCenter.API.Shared.Services.Hashing;
public interface IPasswordHashService
{
    string HashPassword(string password);
    bool VerifyPassword(string password, string hashedPassword);
}
