using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PM_API.Core.Interfaces.Services;
using PM_API.DTOs;
using PM_API.Infrastructure.Model;
using PM_API.Metadata;

namespace PM_API.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class TicketController(ITicketService ticketService, UserManager<User> userManager)
    : ControllerBase
{
    // Services
    private readonly ITicketService _ticketService = ticketService ?? throw new ArgumentNullException(nameof(ticketService));
    
    // Identity
    private readonly UserManager<User> _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
    
    [HttpPost]
    public async Task<ActionResult<Ticket>> CreateTicketAsync([FromBody] TicketDto ticketDto)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
            return Unauthorized();
        
        var userId = user.Id;

        var ticketToCreate = ticketDto.ToModel(userId: userId);
        var ticket = await _ticketService.CreateTicketAsync(ticketToCreate);
        return Ok(new TicketDto(ticket, TicketMetadata.Ticket(ticket.TicketId)));
    }

    [HttpGet("{ticketId}")]
    public async Task<ActionResult<TicketDto>> GetTicketByIdAsync(long ticketId)
    {
        var ticket = await _ticketService.GetTicketById(ticketId);
        return Ok(new TicketDto(ticket, TicketMetadata.Ticket(ticket.TicketId)));
    }
}