using System.ComponentModel.DataAnnotations;

namespace BlazorCrud.Domain;

public class Employee
{
    [Key]
    public Guid Id { get; set; }

    public string Name { get; set; }
    public string Gender { get; set; }
    public string City { get; set; }

    public List<EmployeeContacts> EmployeeContacts { get; set; } = [];
    
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}