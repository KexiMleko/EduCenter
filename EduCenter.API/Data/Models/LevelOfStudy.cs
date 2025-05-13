using System.ComponentModel.DataAnnotations;
using EduCenter.API.Base;

namespace EduCenter.API.Data.Models;
public class LevelOfStudy : BaseEntity
{
    [MaxLength(100)]
    public string Title { get; set; } = "";
    [MaxLength(500)]
    public string? Description { get; set; }
}
