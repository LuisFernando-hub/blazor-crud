using System.Security.Claims;
using BlazorCrud.Domain;
using Microsoft.AspNetCore.Components.Authorization;

namespace BlazorCrud.Service;

public class CustomAuthStateProvider: AuthenticationStateProvider
{
    private ClaimsPrincipal _currentUser = new ClaimsPrincipal(
        new ClaimsIdentity()
    );

    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        return Task.FromResult(
            new AuthenticationState(_currentUser)
        );
    }

    public void Login(User user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.Name, user.Name),
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Role, user.Role.ToString()),
        };

        var identity = new ClaimsIdentity(
            claims,
            "auth_token"
        );

        _currentUser = new ClaimsPrincipal(identity);

        NotifyAuthenticationStateChanged(
            Task.FromResult(
                new AuthenticationState(_currentUser)
            )
        );
    }

    public void Logout()
    {
        _currentUser = new ClaimsPrincipal(
            new ClaimsIdentity()
        );

        NotifyAuthenticationStateChanged(
            Task.FromResult(
                new AuthenticationState(_currentUser)
            )
        );
    }
}