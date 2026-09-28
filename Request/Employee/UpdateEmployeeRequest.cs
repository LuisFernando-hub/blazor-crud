using System.ComponentModel.DataAnnotations;
using BlazorCrud.Enums;

namespace BlazorCrud.Request.Employee;

public class UpdateEmployeeRequest
{
    [Required]
    public Guid Id { get; set; }
    [Required]
    public Guid DepartamentId { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public Gender Gender { get; set; }
    [Required]
    public string City { get; set; }
    
    public EmployeeStatus Status { get; set; } = EmployeeStatus.Active;
    
    public DateTime UpdatedAt { get; set; }
}