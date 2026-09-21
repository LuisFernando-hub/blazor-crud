using System.ComponentModel.DataAnnotations;

namespace BlazorCrud.Request.Employee;

public class CreateEmployeeRequest
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string Gender { get; set; }
    [Required]
    public string City { get; set; }
}