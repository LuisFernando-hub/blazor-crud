using BlazorCrud.Domain;
using BlazorCrud.Enums;
using Microsoft.EntityFrameworkCore;

namespace BlazorCrud.Data;

public class AppDbContext: DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) :base(options) {}
    
    public DbSet<Employee> Employees { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<EmployeeContacts> EmployeeContacts { get; set; }
    public DbSet<Departament> Departaments { get; set; }
    public DbSet<EmployeeNote> EmployeeNotes { get; set; }
    public DbSet<AuditLog> AuditLogs { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Employee>()
            .Property(x => x.Gender)
            .HasConversion<string>();

        modelBuilder.Entity<EmployeeContacts>()
            .Property(x => x.Type)
            .HasConversion<string>();

        modelBuilder.Entity<User>()
            .Property(x => x.Role)
            .HasDefaultValue(UserRole.HR);
        
        modelBuilder.Entity<Employee>()
            .Property(x => x.Status)
            .HasDefaultValue(EmployeeStatus.Active);
        
        modelBuilder.Entity<AuditLog>(entity =>
        {
            entity.HasKey(x => x.Id);

            entity.Property(x => x.Action)
                .HasMaxLength(50)
                .IsRequired();

            entity.Property(x => x.Entity)
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(x => x.Changes)
                .HasColumnType("json");

            entity.HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}