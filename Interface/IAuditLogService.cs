using BlazorCrud.Dtos.Employee;
using BlazorCrud.Request.Employee;

namespace BlazorCrud.Interface;

public interface IAuditLogService
{
    Task LogAsync(string action, string entity, Guid entityId, object? changes = null);
}