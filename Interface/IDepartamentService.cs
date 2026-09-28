using BlazorCrud.Dtos.Department;
using BlazorCrud.Request.Departament;

namespace BlazorCrud.Interface;

public interface IDepartamentService
{
    Task<IEnumerable<DepartamentDTO>> GetAllAsync();
    Task<DepartamentDTO> GetByIdAsync(Guid id);
    Task CreateAsync(CreateDepartamentRequest createEmployee);
    Task UpdateAsync(UpdateDepartamentRequest updateEmployee);
    Task DeleteAsync(Guid id);
}