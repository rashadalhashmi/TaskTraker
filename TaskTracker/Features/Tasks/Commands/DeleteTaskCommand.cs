using MediatR;
using TaskTracker.Repositories;

namespace TaskTracker.Features.Tasks.Commands
{
    public class DeleteTaskCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand>
    {
        private readonly ITaskRepository _taskRepository;

        public DeleteTaskCommandHandler(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
        {
            await _taskRepository.DeleteAsync(request.Id);
        }
    }
}