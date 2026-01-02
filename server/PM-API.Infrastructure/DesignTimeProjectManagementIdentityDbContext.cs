using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace PM_API.Infrastructure;

public class DesignTimeProjectManagementIdentityDbContext : IDesignTimeDbContextFactory<ProjectManagementIdentityDbContext>
{
    public ProjectManagementIdentityDbContext CreateDbContext(string[] args)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        var connectionString = config.GetConnectionString("PMDb")
                               ?? Environment.GetEnvironmentVariable("PM_DB_CONNECTION")
                               ?? "Host=localhost;Database=pm_db;Username=postgres;Password=postgres";

        var optionsBuilder = new DbContextOptionsBuilder<ProjectManagementIdentityDbContext>();
        optionsBuilder.UseNpgsql(connectionString);

        return new ProjectManagementIdentityDbContext(optionsBuilder.Options);
    }
}