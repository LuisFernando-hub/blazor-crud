using BlazorCrud.Dtos.Employee;
using BlazorCrud.Request.Employee;

namespace BlazorCrud.Interface;

public interface IEmployeeService
{
    Task<IEnumerable<EmployeeDTO>> GetAllAsync();
    Task<EmployeeDTO> GetByIdAsync(Guid id);
    Task CreateAsync(CreateEmployeeRequest request);
    Task UpdateAsync(UpdateEmployeeRequest request);
    Task DeleteAsync(Guid id);
}