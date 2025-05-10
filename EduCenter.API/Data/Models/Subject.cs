using EduCenter.API.Base;

namespace EduCenter.API.Data.Models;
public class Subject : BaseEntity
{
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
}
