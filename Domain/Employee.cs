using System.ComponentModel.DataAnnotations;
using BlazorCrud.Enums;

namespace BlazorCrud.Domain;

public class Employee
{
    [Key]
    public Guid Id { get; set; }
    public Guid DepartamentId { get; set; }
    public Departament Departament { get; set; } = null!;
    public string Name { get; set; }
    public Gender Gender { get; set; }
    public string City { get; set; }
    public EmployeeStatus Status { get; set; } = EmployeeStatus.Active;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    public List<EmployeeContacts> EmployeeContacts { get; set; } = [];
}