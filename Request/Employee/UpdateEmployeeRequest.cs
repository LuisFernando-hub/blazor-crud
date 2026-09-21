using System.ComponentModel.DataAnnotations;

namespace BlazorCrud.Request.Employee;

public class UpdateEmployeeRequest
{
    public Guid Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Gender { get; set; }
    [Required]
    public string City { get; set; }
    
    public DateTime UpdatedAt { get; set; }
}