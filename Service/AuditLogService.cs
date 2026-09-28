using System.Security.Claims;
using System.Text.Json;
using BlazorCrud.Data;
using BlazorCrud.Domain;
using BlazorCrud.Dtos.Employee;
using BlazorCrud.Enums;
using BlazorCrud.Interface;
using BlazorCrud.Request.Employee;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;

namespace BlazorCrud.Service;

public class AuditLogService: IAuditLogService
{
    private readonly AppDbContext _context;
    private readonly AuthenticationStateProvider _authenticationStateProvider;

    public AuditLogService(AppDbContext context, AuthenticationStateProvider authenticationStateProvider)
    {
        _context = context;
        _authenticationStateProvider = authenticationStateProvider;
    }
    
    public async Task LogAsync(string action, string entity, Guid entityId, object? changes = null)
    {
        var authState = await _authenticationStateProvider
            .GetAuthenticationStateAsync();

        var user = authState.User;

        var userId = user.FindFirst(
            ClaimTypes.NameIdentifier
        )?.Value;
        
        var auditLog = new AuditLog
        {
            Id = Guid.NewGuid(),
            UserId = Guid.Parse(userId!),

            Action = action,
            Entity = entity,
            EntityId = entityId,
            Changes = changes == null ? null : JsonSerializer.Serialize(changes),
            CreatedAt = DateTime.UtcNow
        };

        _context.AuditLogs.Add(auditLog);

        await _context.SaveChangesAsync();
    }
}