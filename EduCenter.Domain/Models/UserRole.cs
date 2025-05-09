namespace EduCenter.Domain.Models;
public class UserRole : BaseEntity
{
    public int RoleId { get; set; }
    public int UserId { get; set; }
}
