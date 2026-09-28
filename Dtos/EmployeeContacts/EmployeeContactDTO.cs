using BlazorCrud.Dtos.Employee;
using BlazorCrud.Enums;

namespace BlazorCrud.Dtos.EmployeeContacts;

public record EmployeeContactDTO
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }
    public EmployeeDTO Employee { get; set; }
    public ContactTypes Type { get; set; }
    public string Value { get; set; }
    public bool IsPrimary { get; set; } 
    
    public DateTime CreatedAt { get; set; }
    // public DateTime UpdatedAt { get; set; }
}