using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EduCenter.API.Base;

namespace EduCenter.API.Data.Models;
public class RefreshToken : BaseEntity
{
    public int UserId { get; set; }
    [MaxLength(1000)]
    [ForeignKey(nameof(UserId))]
    public User User { get; set; }
    public string Token { get; set; } = "";
    public DateTime ExpiresAt { get; set; }
}
