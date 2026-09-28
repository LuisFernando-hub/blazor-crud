using BlazorCrud.Data;
using BlazorCrud.Domain;
using BlazorCrud.Dtos.Department;
using BlazorCrud.Dtos.Employee;
using BlazorCrud.Enums;
using BlazorCrud.Interface;
using BlazorCrud.Request.Employee;
using Microsoft.EntityFrameworkCore;

namespace BlazorCrud.Service;

public class EmployeeService: IEmployeeService
{
    private readonly AppDbContext _appDbContext;
    private readonly IAuditLogService _auditLogService;

    public EmployeeService(AppDbContext appDbContext, IAuditLogService auditLogService)
    {
        _appDbContext = appDbContext;
        _auditLogService = auditLogService;
    }
    
    public async Task<IEnumerable<EmployeeDTO>> GetAllAsync()
    {
        return await _appDbContext.Employees
            .Include(e => e.Departament)
            .Select(e => new EmployeeDTO()
            {
                Id = e.Id,
                DepartamentId = e.DepartamentId,
                Departament = new DepartamentDTO()
                {
                  Id = e.Departament.Id,
                  Name = e.Departament.Name,
                },
                Name = e.Name,
                Gender = e.Gender,
                City = e.City,
                Status = e.Status,
                CreatedAt = e.CreatedAt
            }).ToListAsync();
    }

    public async Task<EmployeeDTO> GetByIdAsync(Guid id)
    {
        var employee = await _appDbContext.Employees
            .Include(e => e.Departament)
            .FirstOrDefaultAsync(e => e.Id == id); {
            if (employee == null) {
                throw new KeyNotFoundException("Employee not found");
            }
        }

        return new EmployeeDTO
        {
            Id = employee.Id,
            DepartamentId = employee.DepartamentId,
            Name = employee.Name,
            Gender = employee.Gender,
            City = employee.City,
            CreatedAt = employee.CreatedAt
        };
    }

    public async Task CreateAsync(CreateEmployeeRequest request)
    {
        var employee = new Employee
        {
            Id = Guid.NewGuid(),
            DepartamentId = request.DepartamentId,
            Name = request.Name,
            Gender = request.Gender,
            City = request.City,
            Status = EmployeeStatus.Active,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        
        _appDbContext.Employees.Add(employee);
        await _appDbContext.SaveChangesAsync();
        await _auditLogService.LogAsync(
            "CREATE",
            "Employee",
            employee.Id,
            new
            {
                employee.DepartamentId,
                employee.Name,
                employee.Gender,
                employee.City,
            }
        );
    }

    public async Task UpdateAsync(UpdateEmployeeRequest request)
    {
        var employee = await _appDbContext.Employees.FindAsync(request.Id); {
            if (employee == null) { 
                throw new KeyNotFoundException("Employee not found");
            }

            employee.DepartamentId = request.DepartamentId;
            employee.Name = request.Name;
            employee.Gender = request.Gender;
            employee.City = request.City;
            employee.UpdatedAt = DateTime.UtcNow;
            employee.Status = request.Status;
            await _appDbContext.SaveChangesAsync();
        }
        
        await _auditLogService.LogAsync(
            "UPDATE",
            "Employee",
            employee.Id,
            new
            {
                employee.DepartamentId,
                employee.Name,
                employee.Gender,
                employee.City,
                employee.Status
            }
        );
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
            
            await _auditLogService.LogAsync(
                "DELETE",
                "Employee",
                employee.Id,
                new
                {
                    employee.Id,
                    employee.Name
                }
            );
        }
    }
}