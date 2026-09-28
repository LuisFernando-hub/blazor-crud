using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using BlazorCrud.Enums;

namespace BlazorCrud.Request.Employee;

public class CreateEmployeeRequest
{
    [Required]
    public Guid DepartamentId { get; set; }
    [Required]
    [MinLength(3), MaxLength(150)]
    public string Name { get; set; }
    [Required]
    public Gender Gender { get; set; }
    [Required]
    [MaxLength(120)]
    public string City { get; set; }
}