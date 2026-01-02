using Microsoft.EntityFrameworkCore;
using PM_API.Infrastructure.ModelConfiguration;

namespace PM_API.Infrastructure;

public class ProjectManagementDbContext(DbContextOptions<ProjectManagementDbContext> options) : DbContext(options)
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
        modelBuilder.ApplyConfiguration(new TicketConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}