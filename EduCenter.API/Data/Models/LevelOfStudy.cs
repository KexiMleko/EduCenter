using EduCenter.API.Base;

namespace EduCenter.API.Data.Models;
public class LevelOfStudy : BaseEntity
{
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";

}
