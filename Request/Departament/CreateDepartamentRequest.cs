using System.ComponentModel.DataAnnotations;

namespace BlazorCrud.Request.Departament;

public record CreateDepartamentRequest
{
    
    [Required]
    [MinLength(3), MaxLength(150)]
    public string Name { get; set; }
    
    [MaxLength(150)]
    public string? Description { get; set; }
}