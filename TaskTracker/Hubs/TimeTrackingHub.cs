using Microsoft.AspNetCore.SignalR;
using TaskTracker.Services;

namespace TaskTracker.Hubs
{
    public class TimeTrackingHub : Hub
    {
        private readonly TimerBackgroundService _timerService;
        private readonly ILogger<TimeTrackingHub> _logger;

        public TimeTrackingHub(
            TimerBackgroundService timerService,
            ILogger<TimeTrackingHub> logger)
        {
            _timerService = timerService;
            _logger = logger;
        }

        public async Task StartTimer(int taskId)
        {
            _timerService.StartTimer(taskId);
            await Groups.AddToGroupAsync(Context.ConnectionId, $"task-{taskId}");
            _logger.LogInformation("Timer started for task {TaskId}", taskId);
        }

        public async Task StopTimer(int taskId)
        {
            _timerService.StopTimer(taskId);
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"task-{taskId}");
            _logger.LogInformation("Timer stopped for task {TaskId}", taskId);
        }

        public async Task GetTimerStatus(int taskId)
        {
            var elapsedTime = _timerService.GetElapsedTime(taskId);
            var isRunning = _timerService.IsTimerRunning(taskId);
            await Clients.Caller.SendAsync("TimerStatus", taskId, elapsedTime.TotalSeconds, isRunning);
        }

        public override async Task OnConnectedAsync()
        {
            _logger.LogInformation("Client connected: {ConnectionId}", Context.ConnectionId);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            _logger.LogInformation("Client disconnected: {ConnectionId}", Context.ConnectionId);
            await base.OnDisconnectedAsync(exception);
        }
    }
} 