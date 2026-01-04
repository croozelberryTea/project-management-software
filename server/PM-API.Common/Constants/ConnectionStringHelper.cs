namespace PM_API.Common.Constants;

public static class ConnectionStringHelper
{
    public static string GetConnectionString()
    {
        var host = Environment.GetEnvironmentVariable("DB_HOST") ?? "localhost";
        var port = Environment.GetEnvironmentVariable("DB_PORT") ?? "5432";
        var database = Environment.GetEnvironmentVariable("DB_DATABASE_NAME") ?? "pm_db";
        var username = Environment.GetEnvironmentVariable("DB_USERNAME") ?? "postgres";
        var password = Environment.GetEnvironmentVariable("DB_PASSWORD") ?? "postgres";

        // Configure command timeout with a safe default (30 seconds), overridable via environment variable.
        var commandTimeoutEnv = Environment.GetEnvironmentVariable("DB_COMMAND_TIMEOUT");
        var commandTimeout = 30;
        if (!string.IsNullOrWhiteSpace(commandTimeoutEnv)
            && int.TryParse(commandTimeoutEnv, out var parsedTimeout)
            && parsedTimeout > 0
            && parsedTimeout <= 300)
        {
            commandTimeout = parsedTimeout;
        }
    
        return $"Host={host};Port={port};Database={database};Username={username};Password={password};Command Timeout={commandTimeout};";
    }
}