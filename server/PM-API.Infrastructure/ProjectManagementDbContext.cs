using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PM_API.Infrastructure.ModelConfiguration;

namespace PM_API.Infrastructure;

public class ProjectManagementDbContext(DbContextOptions<ProjectManagementDbContext> options) : IdentityDbContext<IdentityUser>(options)
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Only configure if DI didn't provide options
        if (optionsBuilder.IsConfigured) return;

        // Prefer an environment variable, with a sensible local fallback
        // todo set the fallback to something that makes sense or throw.
        var connectionString = Environment.GetEnvironmentVariable("PM_DB_CONNECTION")
                               ?? "Host=localhost;Database=pm_db;Username=postgres;Password=postgres";

        optionsBuilder.UseNpgsql(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema("project");
        modelBuilder.ApplyConfiguration(new TicketConfiguration());
        
        // Apply configuration for Identity Tables
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new RoleConfiguration());
        modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
        modelBuilder.ApplyConfiguration(new UserClaimConfiguration());
        modelBuilder.ApplyConfiguration(new UserLoginConfiguration());
        modelBuilder.ApplyConfiguration(new RoleClaimConfiguration());
        modelBuilder.ApplyConfiguration(new UserTokenConfiguration());
    }
}