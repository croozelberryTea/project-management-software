using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PM_API.Common.Constants;
using PM_API.Infrastructure.Model;
using PM_API.Infrastructure.ModelConfiguration;

namespace PM_API.Infrastructure;

public class ProjectManagementDbContext(DbContextOptions<ProjectManagementDbContext> options) : IdentityDbContext<User, IdentityRole<long>, long>(options)
{
    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<TicketAttachment> TicketAttachments { get; set; }
    public DbSet<TicketComment> TicketComments { get; set; }
    public DbSet<TicketHistory> TicketHistories { get; set; }
    public DbSet<TicketLinkedTicket> TicketLinkedTickets { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Only configure if DI didn't provide options
        if (optionsBuilder.IsConfigured) return;

        // Prefer an environment variable, with a sensible local fallback
        var connectionString = ConnectionStringHelper.GetConnectionString();

        optionsBuilder.UseNpgsql(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema("project");
        modelBuilder.ApplyConfiguration(new TicketConfiguration());
        modelBuilder.ApplyConfiguration(new TicketAttachmentConfiguration());
        modelBuilder.ApplyConfiguration(new TicketCommentConfiguration());
        modelBuilder.ApplyConfiguration(new TicketHistoryConfiguration());
        modelBuilder.ApplyConfiguration(new TicketLinkedTicketConfiguration());
        
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