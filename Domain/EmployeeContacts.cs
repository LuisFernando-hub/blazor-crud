using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Contracts;

namespace BlazorCrud.Domain;

public class EmployeeContacts
{
    [Key]
    public Guid Id { get; set; }
    
    public Guid EmployeeId { get; set; }
    public Employee Employee { get; set; } = null!;
    public string Type { get; set; }
    public string Value { get; set; }
    public bool IsPrimary { get; set; }
}