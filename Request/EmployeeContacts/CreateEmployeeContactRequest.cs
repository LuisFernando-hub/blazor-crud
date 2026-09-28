using System.ComponentModel.DataAnnotations;
using BlazorCrud.Enums;

namespace BlazorCrud.Request.EmployeeContacts;

public record CreateEmployeeContactRequest
{
    [Required]
    public Guid EmployeeId { get; set; }
    [Required]
    public ContactTypes Type { get; set; }
    [Required]
    public string Value { get; set; }
    [Required]
    public bool IsPrimary { get; set; }
}