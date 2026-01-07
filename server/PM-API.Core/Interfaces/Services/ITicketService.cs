using PM_API.Infrastructure.Model;

namespace PM_API.Core.Interfaces.Services;

public interface ITicketService
{
    // Create
    Task<Ticket> CreateTicketAsync(Ticket ticket);
    
    // Read
    Task<Ticket> GetTicketById(long ticketId);
    Task<List<Ticket>> GetTicketsByCreatedUserId(long userId);
    Task<List<Ticket>> GetTicketsByAssignedUserId(long assigneeId);
    
    // Update
    Task<Ticket> UpdateTicketAsync(long ticketId, Ticket ticket);
    Task<Ticket> AssignTicketAsync(long ticketId, long assigneeId);
    Task<Ticket> UnassignTicketAsync(long ticketId);
    
    // Delete
    Task DeleteTicketAsync(long ticketId);
}