using MudBlazor.Services;
using BlazorCrud.Components;
using BlazorCrud.Data;
using BlazorCrud.Interface;
using BlazorCrud.Service;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add MudBlazor services
builder.Services.AddMudServices();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddAuthentication("auth_token")
    .AddCookie("auth_token",options =>
    {
        // options.Cookie.Name = "auth_token";
        options.LoginPath = "/login";
        options.Cookie.MaxAge = TimeSpan.FromDays(1);
        options.AccessDeniedPath = "/access-denied";
    });

builder.Services.AddScoped<CustomAuthStateProvider>();

builder.Services.AddScoped<AuthenticationStateProvider>(
    sp => sp.GetRequiredService<CustomAuthStateProvider>()
);

builder.Services.AddAuthorization();
builder.Services.AddCascadingAuthenticationState();


// 3. Registra o serviço
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IEmployeeContactsService, EmployeeContactsService>();

//Conection database mysql
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseMySql(builder.Configuration.GetConnectionString("mysqlConnection"), 
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("mysqlConnection")));
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.UseAntiforgery();


app.MapStaticAssets();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
