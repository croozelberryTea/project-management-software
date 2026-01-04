namespace PM_API.Infrastructure.Model;

public class TicketComment
{
    public long TicketCommentId { get; set; }
    public string? Comment { get; set; }
    public DateTime CreatedDateTime { get; set; }
    public long UserId { get; set; }
    public long TicketId { get; set; }
    
    public virtual User User { get; set; }
    public virtual Ticket Ticket { get; set; }
}