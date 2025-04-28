using Microsoft.AspNetCore.SignalR;
using TaskTracker.Hubs;
using TaskTracker.Models;

namespace TaskTracker.Services;

public class TimerBackgroundService : BackgroundService
{
    private readonly IHubContext<TimeTrackingHub> _hubContext;
    private readonly ILogger<TimerBackgroundService> _logger;
    private readonly Dictionary<int, DateTime> _activeTimers = new();

    public TimerBackgroundService(
        IHubContext<TimeTrackingHub> hubContext,
        ILogger<TimerBackgroundService> logger)
    {
        _hubContext = hubContext;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await UpdateActiveTimers();
            await Task.Delay(1000, stoppingToken); // Update every second
        }
    }

    private async Task UpdateActiveTimers()
    {
        var now = DateTime.UtcNow;
        foreach (var timer in _activeTimers.ToList())
        {
            var elapsedTime = now - timer.Value;
            await _hubContext.Clients.Group($"task-{timer.Key}")
                .SendAsync("TimerUpdated", timer.Key, elapsedTime.TotalSeconds);
        }
    }

    public void StartTimer(int taskId)
    {
        if (!_activeTimers.ContainsKey(taskId))
        {
            _activeTimers[taskId] = DateTime.UtcNow;
        }
    }

    public void StopTimer(int taskId)
    {
        if (_activeTimers.TryGetValue(taskId, out var startTime))
        {
            _activeTimers.Remove(taskId);
        }
    }

    public TimeSpan GetElapsedTime(int taskId)
    {
        if (_activeTimers.TryGetValue(taskId, out var startTime))
        {
            return DateTime.UtcNow - startTime;
        }
        return TimeSpan.Zero;
    }

    public bool IsTimerRunning(int taskId)
    {
        return _activeTimers.ContainsKey(taskId);
    }
} 