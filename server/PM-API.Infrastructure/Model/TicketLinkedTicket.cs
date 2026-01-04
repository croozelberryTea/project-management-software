using PM_API.Common.Constants.Enums;

namespace PM_API.Infrastructure.Model;

public class TicketLinkedTicket
{
    public long TicketLinkedTicketId { get; set; }
    public TicketRelationshipEnum Relation { get; set; }  // This needs to serialize and deserialize as a int
    public long ParentTicketId { get; set; }
    public long ChildTicketId { get; set; }
    
    public Ticket ParentTicket { get; set; }
    public Ticket ChildTicket { get; set; }
}