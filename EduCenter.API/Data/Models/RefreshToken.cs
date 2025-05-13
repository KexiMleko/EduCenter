using System.ComponentModel.DataAnnotations;
using EduCenter.API.Base;

namespace EduCenter.API.Data.Models;
public class RefreshToken : BaseEntity
{
    public int UserId { get; set; }
    [MaxLength(1000)]
    public string Token { get; set; } = "";
    public DateTime ExpiresAt { get; set; }
}
