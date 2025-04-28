using TaskTracker.Models;
using TaskTracker.Repositories;

namespace TaskTracker.Services
{
    public class TimeTrackerService : ITimeTrackerService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly Dictionary<int, DateTime> _activeTimers = new();
        private readonly Dictionary<int, TimeSpan> _elapsedTimes = new();

        public TimeTrackerService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async System.Threading.Tasks.Task StartTimerAsync(int taskId)
        {
            if (!_activeTimers.ContainsKey(taskId))
            {
                _activeTimers[taskId] = DateTime.UtcNow;
                var task = await _taskRepository.GetByIdAsync(taskId);
                if (task != null)
                {
                    task.Status = TaskState.InProgress;
                    await _taskRepository.UpdateAsync(task);
                }
            }
        }

        public async System.Threading.Tasks.Task StopTimerAsync(int taskId)
        {
            if (_activeTimers.TryGetValue(taskId, out var startTime))
            {
                var elapsed = DateTime.UtcNow - startTime;
                _activeTimers.Remove(taskId);

                if (!_elapsedTimes.ContainsKey(taskId))
                {
                    _elapsedTimes[taskId] = TimeSpan.Zero;
                }

                _elapsedTimes[taskId] += elapsed;

                var task = await _taskRepository.GetByIdAsync(taskId);
                if (task != null)
                {
                    task.TotalTimeSpent = _elapsedTimes[taskId];
                    await _taskRepository.UpdateAsync(task);
                }
            }
        }

        public TimeSpan GetElapsedTime(int taskId)
        {
            var totalTime = _elapsedTimes.GetValueOrDefault(taskId, TimeSpan.Zero);

            if (_activeTimers.TryGetValue(taskId, out var startTime))
            {
                totalTime += DateTime.UtcNow - startTime;
            }

            return totalTime;
        }

        public bool IsTimerRunning(int taskId)
        {
            return _activeTimers.ContainsKey(taskId);
        }
    }
}