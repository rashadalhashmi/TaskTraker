using MediatR;
using TaskTracker.Models;
using TaskTracker.Repositories;

namespace TaskTracker.Features.Tasks.Commands
{
    public class UpdateTaskCommand : IRequest<TaskItem>
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public TaskState Status { get; set; }
        public int EstimatedEffort { get; set; }
    }

    public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand, TaskItem>
    {
        private readonly ITaskRepository _taskRepository;

        public UpdateTaskCommandHandler(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<TaskItem> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            var task = await _taskRepository.GetByIdAsync(request.Id);
            if (task == null)
            {
                throw new System.Exception($"Task with ID {request.Id} not found.");
            }

            task.Title = request.Title;
            task.Description = request.Description;
            task.Status = request.Status;
            task.EstimatedEffort = request.EstimatedEffort;

            return await _taskRepository.UpdateAsync(task);
        }
    }
}