using MediatR;
using TaskTracker.Models;
using TaskTracker.Repositories;

namespace TaskTracker.Features.Tasks.Commands
{
    public class CreateTaskCommand : IRequest<TaskItem>
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int EstimatedEffort { get; set; }
    }

    public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, TaskItem>
    {
        private readonly ITaskRepository _taskRepository;

        public CreateTaskCommandHandler(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<TaskItem> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            var task = new TaskItem
            {
                Title = request.Title,
                Description = request.Description,
                EstimatedEffort = request.EstimatedEffort,
                Status = TaskState.Backlog
            };

            return await _taskRepository.AddAsync(task);
        }
    }
}