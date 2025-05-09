namespace EduCenter.Domain.Models;
public class RefreshToken : BaseEntity
{
    public int UserId { get; set; }
    public string Token { get; set; } = "";
    public DateTime ExpiresAt { get; set; }
}
