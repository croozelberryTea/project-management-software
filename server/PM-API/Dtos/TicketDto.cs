using System.Text.Json.Serialization;
using PM_API.Infrastructure.Model;
using PM_API.Metadata;

namespace PM_API.DTOs;

public class TicketDto
{
    [JsonIgnore]
    public long TicketId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; } = null!;
    public int Priority { get; set; }
    public DateTime CreatedDateTime { get; set; }
    public DateTime? LastModifiedDateTime { get; set; }
    public long CreatedByUserId { get; set; }
    
    [JsonIgnore]
    public string CreatedByUserDisplayName { get; set; } = String.Empty;
    
    // These should be null if the db didn't return a relation, 0 if none but we had the data
    public int? NumberOfAttachments { get; set; }
    public int? NumberOfComments { get; set; }
    public int? NumberOfHistoryItems { get; set; }
    public int? NumberOfLinkedTickets { get; set; }
    
    public Metadata.Metadata? Metadata { get; set; }
    
    public TicketDto(){}
    
    public TicketDto(Ticket ticket, Metadata.Metadata metadata)
    {
        TicketId = ticket.TicketId;
        Title = ticket.Title;
        Description = ticket.Description;
        Priority = ticket.Priority;
        CreatedDateTime = ticket.CreatedDateTime;
        LastModifiedDateTime = ticket.LastModifiedDateTime;
        CreatedByUserId = ticket.UserId;
        
        // todo verify this works. test with and without includes
        if (ticket.Attachments != null) NumberOfAttachments = ticket.Attachments.Count;
        if (ticket.Comments != null) NumberOfComments = ticket.Comments.Count;
        if (ticket.History != null) NumberOfHistoryItems = ticket.History.Count;
        if (ticket.LinkedTickets != null) NumberOfLinkedTickets = ticket.LinkedTickets.Count;
        
        Metadata = metadata;
    }

    public Ticket ToModel(long? userId = null)
    {
        var ticket = new Ticket
        {
            Title = Title,
            Description = Description,
            Priority = Priority,
            UserId = userId ?? CreatedByUserId
        };
        
        if (Metadata != null)
        {
            var link = Metadata.Location;
            ticket.TicketId = long.Parse(link.Substring(link.LastIndexOf('/') + 1));
        }
        
        return ticket;
    }
}