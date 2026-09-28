using System.ComponentModel.DataAnnotations;

namespace BlazorCrud.Request.EmployeeNotes;

public class CreateEmployeeNotesRequest
{
    [Required]
    public Guid EmployeeId { get; set; }
    [Required]
    [MinLength(10), MaxLength(250)]
    public string Content { get; set; }
}