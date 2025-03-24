using System.Collections.Concurrent;
using ProductManagementSystem.iamnikitakostin.Data;
using ProductManagementSystem.iamnikitakostin.Models;

namespace ProductManagementSystem.iamnikitakostin.Utils;

public class DatabaseLogger : ILogger
{
    private readonly BlockingCollection<Error> _logQueue = new();
    private readonly ApplicationDbContext _context;
    private readonly Task _logTask;
    private readonly CancellationTokenSource _cancellationTokenSource = new();

    public DatabaseLogger(ApplicationDbContext context)
    {
        _context = context;
        _logTask = Task.Run(ProcessLogQueue);
    }

    public IDisposable BeginScope<TState>(TState state) => null;

    public bool IsEnabled(LogLevel logLevel)
    {
        return logLevel >= LogLevel.Information;
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
    {
        if (formatter == null) return;

        string logMessage = formatter(state, exception);
        var logEntry = new Error
        {
            LogLevel = logLevel.ToString(),
            Message = logMessage,
            Timestamp = DateTime.UtcNow
        };

        _logQueue.Add(logEntry);
        Console.WriteLine($"[{logLevel}] {logMessage}");
    }

    private async Task ProcessLogQueue()
    {
        foreach (var logEntry in _logQueue.GetConsumingEnumerable(_cancellationTokenSource.Token))
        {
            try
            {
                _context.Errors.Add(logEntry);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Error] Failed to save log: {ex.Message}");
            }
        }
    }

    public void Dispose()
    {
        _cancellationTokenSource.Cancel();
        _logQueue.CompleteAdding();

        while (!_logQueue.IsCompleted)
        {
            if (_logQueue.TryTake(out var logEntry))
            {
                try
                {
                    _context.Errors.Add(logEntry);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[Error] Failed to save log during shutdown: {ex.Message}");
                }
            }
        }

        _logTask.Wait();
    }
}
