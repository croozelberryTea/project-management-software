namespace PM_API.Infrastructure.Model;

public class TicketAttachment
{
    public long TicketAttachmentId { get; set; }
    public string FileName { get; set; }
    public byte[] FileContent { get; set; }
    public string ContentType { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedDateTime { get; set; }
    public long TicketId { get; set; }
    public long? UserId { get; set; }
    
    public virtual Ticket Ticket { get; set; }
    public virtual User? User { get; set; }
}