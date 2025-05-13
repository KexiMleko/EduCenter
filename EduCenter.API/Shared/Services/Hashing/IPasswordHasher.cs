namespace EduCenter.API.Shared.Services.Hashing;
public interface IPasswordHasher
{
    string HashPassword(string password);
    bool VerifyPassword(string password, string hashedPassword);
}
