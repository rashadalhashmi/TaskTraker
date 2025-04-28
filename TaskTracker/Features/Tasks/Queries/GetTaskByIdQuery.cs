using MediatR;
using TaskTracker.Models;
using TaskTracker.Repositories;

namespace TaskTracker.Features.Tasks.Queries
{
    public class GetTaskByIdQuery : IRequest<TaskItem?>
    {
        public int Id { get; set; }
    }

    public class GetTaskByIdQueryHandler : IRequestHandler<GetTaskByIdQuery, TaskItem?>
    {
        private readonly ITaskRepository _taskRepository;

        public GetTaskByIdQueryHandler(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<TaskItem?> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
        {
            return await _taskRepository.GetByIdAsync(request.Id);
        }
    }
}