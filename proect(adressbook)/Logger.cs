using Serilog;
using System;

public static class Logger
{
    static Logger()
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();
    }

    public static void LogInformation(string message)
    {
        Log.Information(message);
    }

    public static void LogError(string message, Exception ex)
    {
        Log.Error(ex, message);
    }
}
