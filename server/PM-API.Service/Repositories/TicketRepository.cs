using System.Transactions;
using Microsoft.EntityFrameworkCore;
using PM_API.Core.Interfaces.Repositories;
using PM_API.Exceptions;
using PM_API.Infrastructure;
using PM_API.Infrastructure.Model;

namespace PM_API.Service.Repositories;

public class TicketRepository(ProjectManagementDbContext context) : BaseRepository(context), ITicketRepository
{
    public async Task<Ticket> CreateTicket(Ticket ticket)
    {
        _context.Tickets.Add(ticket);
        await _context.SaveChangesAsync();
        return ticket;
    }

    public async Task<Ticket> GetTicketById(long ticketId)
    {
        using var ts = new TransactionScope(TransactionScopeOption.Required, TransactionScopeAsyncFlowOption.Enabled);
        var ticket = await _context.Tickets.AsSplitQuery()
            .Include(ticket => ticket.Attachments)
            .Include(ticket => ticket.Comments)
            .Include(ticket => ticket.History)
            .Include(ticket => ticket.LinkedTickets)
            .FirstOrDefaultAsync(ticket => ticket.TicketId == ticketId);
        ts.Complete();
        
        if (ticket == null) throw new NotFoundException("Could not find requested ticket.");
        
        return ticket;
    }
}