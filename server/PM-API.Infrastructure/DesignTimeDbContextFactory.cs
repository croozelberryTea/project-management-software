using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace PM_API.Infrastructure;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ProjectManagementDbContext>
{
    public ProjectManagementDbContext CreateDbContext(string[] args)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        var connectionString = config.GetConnectionString("PMDb")
                               ?? Environment.GetEnvironmentVariable("PM_DB_CONNECTION")
                               ?? "Host=localhost;Database=pm_db;Username=postgres;Password=postgres";

        var optionsBuilder = new DbContextOptionsBuilder<ProjectManagementDbContext>();
        optionsBuilder.UseNpgsql(connectionString);

        return new ProjectManagementDbContext(optionsBuilder.Options);
    }
}