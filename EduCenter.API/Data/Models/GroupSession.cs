using System.ComponentModel.DataAnnotations;
using EduCenter.API.Base;
using EduCenter.API.Data.Enums;

namespace EduCenter.API.Data.Models;
public class GroupSession : BaseEntity
{
    [MaxLength(100)]
    public string Title { get; set; } = "";
    [MaxLength(500)]
    public string? Description { get; set; }
    public int TeacherId { get; set; }
    public int ClassroomId { get; set; }
    public int GroupId { get; set; }
    public DateTime TimeScheduled { get; set; }
    public int SessionDuration { get; set; }
    public SessionStatus Status { get; set; }
}
