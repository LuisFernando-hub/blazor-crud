using System.ComponentModel.DataAnnotations;

namespace BlazorCrud.Request.EmployeeContacts;

public record UpdateEmployeeContactRequest
{
    [Required]
    public Guid Id { get; set; }
    [Required]
    public Guid EmployeeId { get; set; }
    [Required]
    public string Type { get; set; }
    [Required]
    public string Value { get; set; }
    [Required]
    public bool IsPrimary { get; set; }
}