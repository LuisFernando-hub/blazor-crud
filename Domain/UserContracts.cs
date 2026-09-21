using System.ComponentModel.DataAnnotations;

namespace BlazorCrud.Domain;

public class LoginRequest
{
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Email is not valid")]
    public string Email { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; } = string.Empty;
}

public class RegisterRequest
{
    [Required(ErrorMessage = "The name is required")]
    public string Name { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "The email is required")]
    [EmailAddress(ErrorMessage = "Email is not valid")]
    public string Email { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "The password is required")]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
    public string Password { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "The confirmation password is required")]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match")]
    public string ConfirmPassword { get; set; } = string.Empty;
}