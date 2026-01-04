namespace PM_API.Exceptions;

public class NotFoundException : ProjectManagementException
{
    public NotFoundException() {}
    public NotFoundException(string message) : base(message) {}
}