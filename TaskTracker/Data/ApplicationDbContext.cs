using Microsoft.EntityFrameworkCore;
using TaskTracker.Models;

namespace TaskTracker.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<TaskItem> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed some example tasks with static values
            modelBuilder.Entity<TaskItem>().HasData(
                new TaskItem
                {
                    Id = 1,
                    Title = "Implement user authentication",
                    Description = "Add login and registration functionality",
                    Status = TaskState.Backlog,
                    EstimatedEffort = 120,
                    TotalTimeSpent = TimeSpan.Zero,
                    CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    LastModifiedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new TaskItem
                {
                    Id = 2,
                    Title = "Create task management UI",
                    Description = "Build the main task list and detail views",
                    Status = TaskState.InProgress,
                    EstimatedEffort = 180,
                    TotalTimeSpent = TimeSpan.FromMinutes(30),
                    CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    LastModifiedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new TaskItem
                {
                    Id = 3,
                    Title = "Set up database",
                    Description = "Configure Entity Framework and SQLite",
                    Status = TaskState.Done,
                    EstimatedEffort = 60,
                    TotalTimeSpent = TimeSpan.FromMinutes(45),
                    CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    LastModifiedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                }
            );
        }
    }
}