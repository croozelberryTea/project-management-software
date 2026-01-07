using PM_API.Infrastructure;

namespace PM_API.Service.Repositories;

public abstract class BaseRepository(ProjectManagementDbContext context)
{
    protected readonly ProjectManagementDbContext _context = context;
}