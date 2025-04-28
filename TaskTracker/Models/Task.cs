namespace TaskTracker.Models
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public TaskState Status { get; set; }
        public int EstimatedEffort { get; set; } // in minutes
        public TimeSpan TotalTimeSpent { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? LastModifiedAt { get; set; }
    }

    public enum TaskState
    {
        Backlog,
        InProgress,
        Done
    }
}