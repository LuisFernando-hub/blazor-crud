using System.ComponentModel.DataAnnotations;

namespace BlazorCrud.Dtos.Department;

public record DepartamentDTO
{
    public Guid Id { get; set; }
    public Guid DepartamentId { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
    // public DateTime UpdatedAt { get; set; }
}