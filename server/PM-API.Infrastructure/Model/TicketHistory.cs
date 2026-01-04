namespace PM_API.Infrastructure.Model;

public class TicketHistory
{
    public long TicketHistoryId { get; set; }
    public string Action { get; set; }  // todo if i can figure out the enum stuff do that here too
    public Dictionary<string, string>? Details { get; set; }  // need this to be jsonb :hmmm:
    public DateTime CreatedDateTime { get; set; }
    public long UserId { get; set; }
    public long TicketId { get; set; }
    
    public virtual User User { get; set; }
    public virtual Ticket Ticket { get; set; }
}