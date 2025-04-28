using TaskTracker.Models;
using TaskTracker.Repositories;
using Microsoft.AspNetCore.SignalR;
using TaskTracker.Hubs;

namespace TaskTracker.Services
{
    public class TimeTrackerService : ITimeTrackerService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly TimerBackgroundService _timerService;

        public TimeTrackerService(ITaskRepository taskRepository, TimerBackgroundService timerService)
        {
            _taskRepository = taskRepository;
            _timerService = timerService;
        }

        public async System.Threading.Tasks.Task StartTimerAsync(int taskId)
        {
            var task = await _taskRepository.GetByIdAsync(taskId);
            if (task != null)
            {
                task.Status = TaskState.InProgress;
                await _taskRepository.UpdateAsync(task);
                _timerService.StartTimer(taskId);
            }
        }

        public async System.Threading.Tasks.Task StopTimerAsync(int taskId)
        {
            var task = await _taskRepository.GetByIdAsync(taskId);
            if (task != null)
            {
                var elapsedTime = _timerService.GetElapsedTime(taskId);
                task.TotalTimeSpent += elapsedTime;
                task.Status = TaskState.Backlog;
                await _taskRepository.UpdateAsync(task);
                _timerService.StopTimer(taskId);
            }
        }

        public TimeSpan GetElapsedTime(int taskId)
        {
            return _timerService.GetElapsedTime(taskId);
        }

        public bool IsTimerRunning(int taskId)
        {
            return _timerService.IsTimerRunning(taskId);
        }
    }
}