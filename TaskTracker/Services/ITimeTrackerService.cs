namespace TaskTracker.Services
{
    public interface ITimeTrackerService
    {
        Task StartTimerAsync(int taskId);
        Task StopTimerAsync(int taskId);
        TimeSpan GetElapsedTime(int taskId);
        bool IsTimerRunning(int taskId);
    }
}