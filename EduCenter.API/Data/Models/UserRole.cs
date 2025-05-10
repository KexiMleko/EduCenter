using EduCenter.API.Base;

namespace EduCenter.API.Data.Models;
public class UserRole : BaseEntity
{
    public int RoleId { get; set; }
    public int UserId { get; set; }
}
