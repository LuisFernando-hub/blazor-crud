using System.Security.Claims;
using BlazorCrud.Data;
using BlazorCrud.Domain;
using BlazorCrud.Interface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;

namespace BlazorCrud.Service;

public class AuthService: IAuthService
{
    private readonly AppDbContext _context;
    private readonly HttpClient _httpClient;
    
    public AuthService(
        AppDbContext context
    ) {
        _context = context;
    }
    
    public async Task<(bool Success, string ErrorMessage)> RegisterAsync(RegisterRequest request)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == request.Email);
        if (user != null)
        {
            return (false, "There is already a registered user with this email.");
        }

        var newUser = new User
        {
            Name = request.Name,
            Email = request.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
        };
        
        _context.Users.Add(newUser);
        await _context.SaveChangesAsync();
        return (true, string.Empty);
    }

    public async Task<(bool Success, string ErrorMessage)> LoginAsync(LoginRequest request)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == request.Email);

        if (user == null)
        {
            return (false, "E-mail or password is incorrect.");
        }
        
        bool isPasswordValid = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);
        if (!isPasswordValid)
        {
            return (false, "E-mail or password is incorrect.");
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Email, user.Email),
        };
        
        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);
        
        return (true, string.Empty);
    }

    public Task LogoutAsync()
    {
        return Task.CompletedTask;
    }
}