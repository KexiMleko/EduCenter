using System.Security.Cryptography;
using System.Text;
using Isopoh.Cryptography.Argon2;
using Isopoh.Cryptography.SecureArray;
using Microsoft.Extensions.Options;
namespace EduCenter.API.Shared.Services.Hashing;
public class Argon2PasswordHasher : IPasswordHasher
{
    private readonly Argon2Options _options;
    public Argon2PasswordHasher(IOptions<Argon2Options> options)
    {
        _options = options.Value;
    }
    public string HashPassword(string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(16);

        var config = new Argon2Config
        {
            Type = _options.Type,
            TimeCost = _options.TimeCost,
            MemoryCost = _options.MemoryCost,
            Lanes = _options.Lanes,
            Threads = _options.Threads,
            Salt = salt,
            Password = Encoding.UTF8.GetBytes(password),
            HashLength = _options.HashLength
        };

        var argon2A = new Argon2(config);
        using (SecureArray<byte> hashA = argon2A.Hash())
        {
            return config.EncodeString(hashA.Buffer);
        }
    }

    public bool VerifyPassword(string password, string hashedPassword)
    {
        var configOfPasswordToVerify = new Argon2Config
        {
            Type = _options.Type,
            TimeCost = _options.TimeCost,
            MemoryCost = _options.MemoryCost,
            Lanes = _options.Lanes,
            Threads = _options.Threads,
            Password = Encoding.UTF8.GetBytes(password),
            HashLength = _options.HashLength
        };

        SecureArray<byte>? hashB = null;
        try
        {
            if (!configOfPasswordToVerify.DecodeString(hashedPassword, out hashB) && hashB == null)
                return false;

            var argon2ToVerify = new Argon2(configOfPasswordToVerify);
            using SecureArray<byte>? hashToVerify = argon2ToVerify.Hash();
            return Argon2.FixedTimeEquals(hashB, hashToVerify);
        }
        finally
        {
            hashB?.Dispose();
        }
    }
}
