using System.Security.Claims;
using BlazorCrud.Data;
using BlazorCrud.Domain;
using BlazorCrud.Dtos.Department;
using BlazorCrud.Dtos.Employee;
using BlazorCrud.Dtos.EmployeeContacts;
using BlazorCrud.Dtos.EmployeeNotes;
using BlazorCrud.Interface;
using BlazorCrud.Request.EmployeeNotes;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;

namespace BlazorCrud.Service;

public class EmployeeNotesService: IEmployeeNotesService
{
    private readonly AppDbContext _appDbContext;
    private readonly AuthenticationStateProvider _authenticationStateProvider;
    private readonly IAuditLogService _auditLogService;

    public EmployeeNotesService(AppDbContext appDbContext, AuthenticationStateProvider authenticationStateProvider,
        IAuditLogService auditLogService)
    {
        _appDbContext = appDbContext;
        _authenticationStateProvider = authenticationStateProvider;
        _auditLogService = auditLogService;
    }
    
    public async Task<IEnumerable<EmployeeNotesDTO>> GetAllAsync()
    {
        return await _appDbContext.EmployeeNotes
            .Include(e => e.Employee)
            .Select(e => new EmployeeNotesDTO()
            {
                Id = e.Id,
                Employee = new EmployeeDTO()
                {
                    Id = e.Employee.Id,
                    Name = e.Employee.Name,
                    EmployeeContacts = e.Employee.EmployeeContacts
                        .Select(c => new EmployeeContactDTO()
                        {
                            Id = c.Id,
                            Type = c.Type,
                            Value = c.Value,
                            IsPrimary = c.IsPrimary
                        })
                        .ToList(),
                    Departament = new DepartamentDTO()
                    {
                        Id = e.Employee.Departament.Id,
                        Name = e.Employee.Departament.Name
                    }
                },
                Content = e.Content,
                CreatedAt = e.CreatedAt,
            }).ToListAsync();
    }

    public async Task<EmployeeNotesDTO> GetByIdAsync(Guid id)
    {
        var employeeNote = await _appDbContext.EmployeeNotes
            .Include(e => e.Employee)
            .FirstOrDefaultAsync(e => e.Id == id); {
            if (employeeNote == null) {
                throw new KeyNotFoundException("EmployeeNotes not found");
            }
        }

        return new EmployeeNotesDTO
        {
            Id = employeeNote.Id,
            EmployeeId = employeeNote.EmployeeId,
            Content = employeeNote.Content,
            CreatedAt = employeeNote.CreatedAt,
        };
    }

    public async Task CreateAsync(CreateEmployeeNotesRequest request)
    {
        var authState = await _authenticationStateProvider
            .GetAuthenticationStateAsync();

        var user = authState.User;

        var userId = user.FindFirst(
            ClaimTypes.NameIdentifier
        )?.Value;
        
        var employeeNote = new EmployeeNote()
        {
            Id = Guid.NewGuid(),
            EmployeeId = request.EmployeeId,
            Content = request.Content,
            UserId = Guid.Parse(userId!),
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        
        _appDbContext.EmployeeNotes.Add(employeeNote);
        await _appDbContext.SaveChangesAsync();
        
        await _auditLogService.LogAsync(
            "CREATE",
            "EmployeeNotes",
            employeeNote.Id,
            new
            {
                employeeNote.EmployeeId,
                employeeNote.UserId,
                employeeNote.Content,
            }
        );
    }

    public async Task UpdateAsync(UpdateEmployeeNotesRequest request)
    {
        var authState = await _authenticationStateProvider
            .GetAuthenticationStateAsync();

        var user = authState.User;

        var userId = user.FindFirst(
            ClaimTypes.NameIdentifier
        )?.Value;
        
        var employeeNote = await _appDbContext.EmployeeNotes.FindAsync(request.Id); {
            if (employeeNote == null) { 
                throw new KeyNotFoundException("Employee not found");
            }

            employeeNote.EmployeeId = request.EmployeeId;
            employeeNote.UserId = Guid.Parse(userId!);
            employeeNote.Content = request.Content;
            employeeNote.UpdatedAt = DateTime.UtcNow;
            await _appDbContext.SaveChangesAsync();
        }
        
        await _auditLogService.LogAsync(
            "UPDATE",
            "EmployeeNotes",
            employeeNote.Id,
            new
            {
                employeeNote.EmployeeId,
                employeeNote.UserId,
                employeeNote.Content,
            }
        );
    }

    public async Task DeleteAsync(Guid id)
    {
        var employeeNote = await _appDbContext.EmployeeNotes.FindAsync(id);
        {
            if (employeeNote == null) {
               throw new KeyNotFoundException("Employee not found"); 
            }
            
            _appDbContext.EmployeeNotes.Remove(employeeNote);
            await _appDbContext.SaveChangesAsync();
            
            await _auditLogService.LogAsync(
                "DELETE",
                "EmployeeNotes",
                employeeNote.Id,
                new
                {
                    employeeNote.EmployeeId,
                    employeeNote.UserId,
                    employeeNote.Content,
                }
            );
        }
    }
}