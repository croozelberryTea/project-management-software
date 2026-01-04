using PM_API.Core.Interfaces.Repositories;
using PM_API.Core.Interfaces.Services;
using PM_API.Infrastructure.Model;
using PM_API.Repositories.Interfaces;

namespace PM_API.Service.Services;

public class TicketService : ITicketService
{
    private readonly ITicketRepository _ticketRepository;

    public TicketService(ITicketRepository ticketRepository)
    {
        _ticketRepository = ticketRepository ?? throw new ArgumentNullException(nameof(ticketRepository));
    }
    
    public async Task<Ticket> CreateTicketAsync(Ticket ticket)
    {
        var createdTimeStamp = DateTime.UtcNow;
        ticket.CreatedDateTime = createdTimeStamp;
        ticket.LastModifiedDateTime = createdTimeStamp;

        return await _ticketRepository.CreateTicket(ticket);
    }

    public async Task<Ticket> GetTicketById(long ticketId)
    {
        return await _ticketRepository.GetTicketById(ticketId); 
    }

    public Task<List<Ticket>> GetTicketsByCreatedUserId(long userId)
    {
        throw new NotImplementedException();
    }

    public Task<List<Ticket>> GetTicketsByAssignedUserId(long assigneeId)
    {
        throw new NotImplementedException();
    }

    public Task<Ticket> UpdateTicketAsync(long ticketId, Ticket ticket)
    {
        throw new NotImplementedException();
    }

    public Task<Ticket> AssignTicketAsync(long ticketId, long assigneeId)
    {
        throw new NotImplementedException();
    }

    public Task<Ticket> UnassignTicketAsync(long ticketId)
    {
        throw new NotImplementedException();
    }

    public Task DeleteTicketAsync(long ticketId)
    {
        throw new NotImplementedException();
    }
}