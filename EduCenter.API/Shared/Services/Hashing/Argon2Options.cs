using Isopoh.Cryptography.Argon2;

namespace EduCenter.API.Shared.Services.Hashing;

public class Argon2Options
{
    public Argon2Type Type { get; set; } = Argon2Type.HybridAddressing;
    public int TimeCost { get; set; } = 2;
    public int MemoryCost { get; set; } = 32768;
    public int Lanes { get; set; } = 8;
    public int Threads { get; set; } = 8;
    public int HashLength { get; set; } = 32;
}
