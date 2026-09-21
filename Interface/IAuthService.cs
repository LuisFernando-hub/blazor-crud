using BlazorCrud.Domain;

namespace BlazorCrud.Interface;

public interface IAuthService
{
    Task<(bool Success, string ErrorMessage)> RegisterAsync(RegisterRequest request);
    Task<(bool Success, string ErrorMessage)> LoginAsync(LoginRequest request);
    Task LogoutAsync();
}