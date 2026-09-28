using System.ComponentModel.DataAnnotations;
using BlazorCrud.Enums;

namespace BlazorCrud.Domain;

public class User
{
    [Key]
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;

    public UserRole Role { get; set; } = UserRole.HR;
    
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}