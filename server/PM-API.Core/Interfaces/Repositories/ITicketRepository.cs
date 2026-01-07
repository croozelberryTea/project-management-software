using PM_API.Infrastructure.Model;

namespace PM_API.Core.Interfaces.Repositories;

public interface ITicketRepository
{
    Task<Ticket> CreateTicket(Ticket ticket);
    Task<Ticket> GetTicketById(long ticketId);
}