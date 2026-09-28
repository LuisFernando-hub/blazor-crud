using BlazorCrud.Dtos.Department;
using BlazorCrud.Dtos.EmployeeContacts;
using BlazorCrud.Enums;

namespace BlazorCrud.Dtos.Employee;

public record EmployeeDTO
{
    public Guid Id { get; set; }
    public Guid DepartamentId { get; set; }
    public DepartamentDTO Departament { get; set; }
    public List<EmployeeContactDTO> EmployeeContacts { get; set; }
    public EmployeeStatus Status { get; set; }
    public string Name { get; set; }
    public Gender Gender { get; set; }
    public string City { get; set; }
    
    public DateTime CreatedAt { get; set; }
    // public DateTime UpdatedAt { get; set; }
}