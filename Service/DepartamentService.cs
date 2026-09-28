using BlazorCrud.Data;
using BlazorCrud.Domain;
using BlazorCrud.Dtos.Department;
using BlazorCrud.Interface;
using BlazorCrud.Request.Departament;
using Microsoft.EntityFrameworkCore;

namespace BlazorCrud.Service;

public class DepartamentService: IDepartamentService
{
    private readonly AppDbContext _appDbContext;

    public DepartamentService(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    
    public async Task<IEnumerable<DepartamentDTO>> GetAllAsync()
    {
        return await _appDbContext.Departaments
            .Select(d => new DepartamentDTO()
            {
                Id = d.Id,
                Name = d.Name,
                Description = d.Description,
                CreatedAt = d.CreatedAt,
            }).ToListAsync();
    }

    public async Task<DepartamentDTO> GetByIdAsync(Guid id)
    {
        var departament = await _appDbContext.Departaments.FirstOrDefaultAsync(d => d.Id == id);
        {
            if (departament == null)
            {
                throw new KeyNotFoundException("Departament not found");
            }
        }
        return new DepartamentDTO
        {
            Id = departament.Id,
            Name = departament.Name,
            Description = departament.Description,
        };
    }

    public async Task CreateAsync(CreateDepartamentRequest createEmployee)
    {
        var departament = new Departament
        {
            Id = Guid.NewGuid(),
            Name = createEmployee.Name,
            Description = createEmployee.Description ?? string.Empty,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        
        await _appDbContext.Departaments.AddAsync(departament);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(UpdateDepartamentRequest updateEmployee)
    {
        var departament = await _appDbContext.Departaments
            .FirstOrDefaultAsync(d => d.Id == updateEmployee.Id);
        {
            if (departament == null)
            {
                throw new KeyNotFoundException("Departament not found");
            }
            
            departament.Name = updateEmployee.Name;
            departament.Description = updateEmployee.Description ?? string.Empty;
            departament.UpdatedAt = DateTime.UtcNow;
            
            await _appDbContext.SaveChangesAsync();
        }
    }

    public async Task DeleteAsync(Guid id)
    {
        var departament = await _appDbContext.Departaments
            .Include(d => d.Employees)
            .FirstOrDefaultAsync(d => d.Id == id);
        {
            if (departament == null) {
                throw new KeyNotFoundException("Departament not found"); 
            }

            if (departament.Employees.Count != 0)
            {
                throw new InvalidOperationException("Existing Employees in departament are not allowed");
            }
            
            _appDbContext.Departaments.Remove(departament);
            await _appDbContext.SaveChangesAsync();
        }    
    }
}