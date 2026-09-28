using BlazorCrud.Dtos.Employee;

namespace BlazorCrud.Dtos.EmployeeNotes;

public record EmployeeNotesDTO
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }
    public EmployeeDTO Employee { get; set; }
    public string Content { get; set; }
    
    public DateTime CreatedAt { get; set; }
    // public DateTime UpdatedAt { get; set; }
}