using BlazorCrud.Data;
using BlazorCrud.Domain;
using BlazorCrud.Dtos.Employee;
using BlazorCrud.Dtos.EmployeeContacts;
using BlazorCrud.Interface;
using BlazorCrud.Request.EmployeeContacts;
using Microsoft.EntityFrameworkCore;

namespace BlazorCrud.Service;

public class EmployeeContactsService : IEmployeeContactsService
{
    private readonly AppDbContext _appDbContext;

    public EmployeeContactsService(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    
    public async Task<IEnumerable<EmployeeContactDTO>> GetAllAsync()
    {
        return await _appDbContext.EmployeeContacts
            .Include(e => e.Employee)
            .Select(ec => new EmployeeContactDTO()
            {
                Id = ec.Id,
                EmployeeId =  ec.Employee.Id,
                Employee = new EmployeeDTO()
                {
                    Id = ec.Employee.Id,
                    Name = ec.Employee.Name,
                },
                Type = ec.Type,
                Value = ec.Value,
                IsPrimary = ec.IsPrimary,
            }).ToListAsync();
    }

    public async Task<EmployeeContactDTO> GetByIdAsync(Guid id)
    {
        var employeeContact = await _appDbContext.EmployeeContacts
            .Include(e => e.Employee).FirstOrDefaultAsync(e => e.Id == id);
        {
            if (employeeContact == null)
            {
                throw new KeyNotFoundException("Employee Contact not found");
            }
        }

        return new EmployeeContactDTO
        {
            Id = employeeContact.Id,
            Employee = new EmployeeDTO()
            {
                Id = employeeContact.Employee.Id,
                Name = employeeContact.Employee.Name,
            },
            Type = employeeContact.Type,
            Value = employeeContact.Value,
            IsPrimary = employeeContact.IsPrimary,
        };
    }

    public async Task CreateAsync(CreateEmployeeContactRequest request)
    {
        //Validar se o tipo do contato é primario e se ja existe um tipo primeiro
        var checkTypePrimary = await _appDbContext.EmployeeContacts
            .Include(e => e.Employee)
            .FirstOrDefaultAsync(ec => ec.Type == request.Type && ec.IsPrimary == true);
        
        if (checkTypePrimary != null)
        {
            throw new InvalidOperationException(
                $"Employee Contact type {checkTypePrimary.Type} " +
                $"primary is already in use in employee {checkTypePrimary.Employee.Name}"
                );
        }
        
        var employeeContact = new EmployeeContacts
        {
            Id = Guid.NewGuid(),
            Type = request.Type,
            Value = request.Value,
            IsPrimary = request.IsPrimary,
            EmployeeId = request.EmployeeId
        };
        
        _appDbContext.EmployeeContacts.Add(employeeContact);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(UpdateEmployeeContactRequest request)
    {
        var employeeContacts = await _appDbContext.EmployeeContacts
            .FirstOrDefaultAsync(e => e.Id == request.Id);
        {
            if (employeeContacts == null)
            {
                throw new KeyNotFoundException("Employee Contact not found");
            }
            
            employeeContacts.Type = request.Type;
            employeeContacts.Value = request.Value;
            employeeContacts.IsPrimary = request.IsPrimary;
            employeeContacts.EmployeeId = request.EmployeeId;
            await _appDbContext.SaveChangesAsync();
        }
    }

    public async Task DeleteAsync(Guid id)
    {
        var employeeContact = await _appDbContext.EmployeeContacts.FindAsync(id);
        {
            if (employeeContact == null) {
                throw new KeyNotFoundException("Employee Contact not found"); 
            }
            
            _appDbContext.EmployeeContacts.Remove(employeeContact);
            await _appDbContext.SaveChangesAsync();
        }    
    }
}