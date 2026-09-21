namespace BlazorCrud.Dtos.Employee;

public record EmployeeDTO
{
    public Guid Id { get; set; }

    public string Name { get; set; }
    public string Gender { get; set; }
    public string City { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}