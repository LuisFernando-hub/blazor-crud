using BlazorCrud.Domain;
using Microsoft.EntityFrameworkCore;

namespace BlazorCrud.Data;

public class AppDbContext: DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) :base(options) {}
    
    public DbSet<Employee> Employees { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<EmployeeContacts> EmployeeContacts { get; set; }
}