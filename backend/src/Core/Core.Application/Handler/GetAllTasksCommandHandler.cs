using Core.Application.Command;
using Core.Domain.Entities;
using Infrastructure.Repository.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Handler
{
    public class GetAllTasksCommandHandler : IRequestHandler<GetAllTasksCommand, IEnumerable<TaskResponseModel>>
    {
        private readonly ITaskRepository _taskRepository;

        public GetAllTasksCommandHandler(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<IEnumerable<TaskResponseModel>> Handle(GetAllTasksCommand request, CancellationToken cancellationToken)
        {
            var tasks = await _taskRepository.GetAllAsync();

            return tasks.Select(task => new TaskResponseModel
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                IsCompleted = task.IsCompleted,
                CreatedAt = task.CreatedAt,
                UpdatedAt = task.UpdatedAt
            });
        }
    }
}
