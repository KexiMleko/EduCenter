using EduCenter.API.Base;
using EduCenter.API.Data.Enums;

namespace EduCenter.API.Data.Models;
public class GroupSession : BaseEntity
{
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public int TeacherId { get; set; }
    public int GroupId { get; set; }
    public SessionStatus Status { get; set; }
}
