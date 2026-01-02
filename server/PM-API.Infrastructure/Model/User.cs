namespace PM_API.Infrastructure.Model;

public class User
{
    public long UserId { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedDateTime { get; set; }
    public DateTime? LastModifiedDateTime { get; set; }
}