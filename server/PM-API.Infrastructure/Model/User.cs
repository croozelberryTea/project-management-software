using Microsoft.AspNetCore.Identity;
using PM_API.Infrastructure.Model;

namespace PM_API.Infrastructure.Model;

public class User : IdentityUser<long>
{
    public List<Ticket> Tickets { get; set; } = [];
}