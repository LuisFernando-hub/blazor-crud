using System.ComponentModel.DataAnnotations;

namespace BlazorCrud.Domain;

public class AuditLog
{
    [Key]
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
    public string Action { get; set; } = string.Empty;
    public string Entity  { get; set; }  = string.Empty;
    public Guid EntityId { get; set; }
    public string? Changes { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}