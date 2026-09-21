using BlazorCrud.Domain;
using BlazorCrud.Dtos.EmployeeContacts;
using BlazorCrud.Request.EmployeeContacts;

namespace BlazorCrud.Interface;

public interface IEmployeeContactsService
{
    Task<IEnumerable<EmployeeContactDTO>> GetAllAsync();
    Task<EmployeeContactDTO> GetByIdAsync(Guid id);
    Task CreateAsync(CreateEmployeeContactRequest createEmployee);
    Task UpdateAsync(UpdateEmployeeContactRequest updateEmployee);
    Task DeleteAsync(Guid id);
}