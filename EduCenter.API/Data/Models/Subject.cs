using System.ComponentModel.DataAnnotations;
using EduCenter.API.Base;

namespace EduCenter.API.Data.Models;
public class Subject : BaseEntity
{
    [MaxLength(100)]
    public string Name { get; set; } = "";
    [MaxLength(1000)]
    public string Description { get; set; } = "";
}
