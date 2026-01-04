namespace PM_API.Metadata;

public static class TicketMetadata
{
    public static Metadata Ticket(long ticketId) => new Metadata
    {
        // Todo need to get the base url
        Location = $"/ticket/{ticketId}",
        AllowedMethods = new List<string> { "GET", "PUT", "DELETE" },
    };
}