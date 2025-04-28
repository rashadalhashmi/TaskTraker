using MediatR;
using TaskTracker.Models;
using TaskTracker.Repositories;

namespace TaskTracker.Features.Tasks.Queries
{
    public class GetAllTasksQuery : IRequest<List<TaskItem>>
    {
    }

    public class GetAllTasksQueryHandler : IRequestHandler<GetAllTasksQuery, List<TaskItem>>
    {
        private readonly ITaskRepository _taskRepository;

        public GetAllTasksQueryHandler(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<List<TaskItem>> Handle(GetAllTasksQuery request, CancellationToken cancellationToken)
        {
            return await _taskRepository.GetAllAsync();
        }
    }
}