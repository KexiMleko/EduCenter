using System.Text.Json;
using System.Text.Json.Serialization;
using EduCenter.API.Data.Models;

namespace EduCenter.API.Features.Users.DTOs;
public class UserDetailsViewModel
{
    public int Id { get; set; }
    public string Username { get; set; } = "";
    public string Email { get; set; } = "";
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public string PhoneNumber { get; set; } = "";
    public string Address { get; set; } = "";
    public string? Note { get; set; } = "";
    public bool IsActive { get; set; }
    [JsonIgnore]
    public string UserRolesJson { get; set; } = "";
    public List<Role> UserRoles =>
            JsonSerializer.Deserialize<List<Role>>(UserRolesJson) ?? new();
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
