using System.ComponentModel.DataAnnotations;

namespace BlazorCrud.Domain;

public class EmployeeNote
{
    [Key]
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }
    public Employee Employee { get; set; } = null!;
    
    public Guid UserId { get; set; }
    public string Content { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}