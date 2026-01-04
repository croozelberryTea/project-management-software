namespace PM_API.Exceptions;

public class ProjectManagementException : System.Exception
{
    public ProjectManagementException() {}
    public ProjectManagementException(string message) : base(message) {}
}