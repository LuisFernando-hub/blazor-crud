using BlazorCrud.Data;
using BlazorCrud.Domain;
using BlazorCrud.Dtos.Employee;
using BlazorCrud.Interface;
using BlazorCrud.Request.Employee;
using Microsoft.EntityFrameworkCore;

namespace BlazorCrud.Service;

public class EmployeeService: IEmployeeService
{
    private readonly AppDbContext _appDbContext;

    public EmployeeService(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    
    public async Task<IEnumerable<EmployeeDTO>> GetAllAsync()
    {
        return await _appDbContext.Employees
            .Select(e => new EmployeeDTO()
            {
                Id = e.Id,
                Name = e.Name,
                Gender = e.Gender,
                City = e.City,
                CreatedAt = e.CreatedAt,
                UpdatedAt = e.UpdatedAt
            }).ToListAsync();
    }

    public async Task<EmployeeDTO> GetByIdAsync(Guid id)
    {
        var employee = await _appDbContext.Employees.FindAsync(id); {
            if (employee == null) {
                throw new KeyNotFoundException("Employee not found");
            }
        }
        

        return new EmployeeDTO
        {
            Id = employee.Id,
            Name = employee.Name,
            Gender = employee.Gender,
            City = employee.City,
            CreatedAt = employee.CreatedAt,
            UpdatedAt = employee.UpdatedAt
        };
    }

    public async Task CreateAsync(CreateEmployeeRequest request)
    {
        var employee = new Employee
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Gender = request.Gender,
            City = request.City,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        
        _appDbContext.Employees.Add(employee);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(UpdateEmployeeRequest request)
    {
        var employee = await _appDbContext.Employees.FindAsync(request.Id); {
            if (employee == null) { 
                throw new KeyNotFoundException("Employee not found");
            }
            
            employee.Name = request.Name;
            employee.Gender =  request.Gender;
            employee.City = request.City;
            employee.UpdatedAt =  DateTime.UtcNow;
        }
    }

    public async Task DeleteAsync(Guid id)
    {
        var employee = await _appDbContext.Employees.FindAsync(id);
        {
            if (employee == null) {
               throw new KeyNotFoundException("Employee not found"); 
            }
            
            _appDbContext.Employees.Remove(employee);
            await _appDbContext.SaveChangesAsync();
        }
    }
}