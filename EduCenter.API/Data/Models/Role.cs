using System.ComponentModel.DataAnnotations;
using EduCenter.API.Base;

namespace EduCenter.API.Data.Models;
public class Role : BaseEntity
{
    [MaxLength(50)]
    public string Name { get; set; } = "";
}
