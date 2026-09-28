using BlazorCrud.Dtos.EmployeeNotes;
using BlazorCrud.Request.EmployeeNotes;

namespace BlazorCrud.Interface;

public interface IEmployeeNotesService
{
    Task<IEnumerable<EmployeeNotesDTO>> GetAllAsync();
    Task<EmployeeNotesDTO> GetByIdAsync(Guid id);
    Task CreateAsync(CreateEmployeeNotesRequest request);
    Task UpdateAsync(UpdateEmployeeNotesRequest request);
    Task DeleteAsync(Guid id);
}