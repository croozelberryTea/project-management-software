using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PM_API.Infrastructure;

public class ProjectManagementIdentityDbContext : IdentityDbContext<IdentityUser>
{
    public ProjectManagementIdentityDbContext(DbContextOptions<ProjectManagementIdentityDbContext> options) :
        base(options)
    { }
}