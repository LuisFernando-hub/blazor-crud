using System.ComponentModel.DataAnnotations;

namespace BlazorCrud.Request.EmployeeNotes;

public class UpdateEmployeeNotesRequest
{
    [Required]
    public Guid Id { get; set; }
    [Required]
    public Guid EmployeeId { get; set; }
    [Required]
    [MinLength(10), MaxLength(250)]
    public string Content { get; set; }
}