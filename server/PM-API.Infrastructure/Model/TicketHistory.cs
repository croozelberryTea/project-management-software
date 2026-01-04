using PM_API.Common.Constants.Enums;

namespace PM_API.Infrastructure.Model;

public class TicketHistory
{
    public long TicketHistoryId { get; set; }
    public HistoryAction Action { get; set; }
    public Dictionary<string, string>? Details { get; set; }
    public DateTime CreatedDateTime { get; set; }
    public long UserId { get; set; }
    public long TicketId { get; set; }
    
    public virtual User User { get; set; }
    public virtual Ticket Ticket { get; set; }
}