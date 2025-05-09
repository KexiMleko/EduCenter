using EduCenter.Domain.Enums;

namespace EduCenter.Domain.Models;
public class GroupSession : BaseEntity
{
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public int TeacherId { get; set; }
    public int GroupId { get; set; }
    public SessionStatus Status { get; set; }
}
