using System.ComponentModel.DataAnnotations;

namespace BlazorCrud.Domain;

public class Departament
{
    [Key]
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    public List<Employee> Employees { get; set; } = [];
}