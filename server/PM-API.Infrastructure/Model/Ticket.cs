namespace PM_API.Infrastructure.Model;

public partial class Ticket
{
    public long TicketId { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int Priority { get; set; }
    public DateTime CreatedDateTime { get; set; }
    public DateTime? LastModifiedDateTime { get; set; }
    public long UserId { get; set; }
    
    public virtual User User { get; set; }
    public virtual ICollection<TicketAttachment> Attachments { get; set; } = new List<TicketAttachment>();
    public virtual ICollection<TicketComment> Comments { get; set; } = new List<TicketComment>();
    public virtual ICollection<TicketHistory> History { get; set; } = new List<TicketHistory>();
    public virtual ICollection<TicketLinkedTicket> LinkedTickets { get; set; } = new List<TicketLinkedTicket>();
}