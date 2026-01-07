namespace PM_API.DTOs;

public class HttpErrorDto(
    string errorMessage,
    bool isNotFound = false,
    bool isInternalError = false,
    bool isConflict = false)
{
    public string ErrorMessage { get; set; } = errorMessage;
    public bool IsNotFound { get; set; } = isNotFound;
    public bool InternalError { get; set; } = isInternalError;
    public bool Conflict { get; set; } = isConflict;
}