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
    public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, TaskResponseModel>
    {
        private readonly ITaskRepository _taskRepository;
        public CreateTaskCommandHandler(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }
        public async Task<TaskResponseModel> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            var newTask = new TaskItem
            {
                Id = Guid.NewGuid().ToString(),
                Title = request.Title,
                Description = request.Description,
                IsCompleted = false,
                CreatedAt = DateTime.UtcNow
            };

            await _taskRepository.CreateAsync(newTask);

            return new TaskResponseModel
            {
                Id = newTask.Id,
                Title = newTask.Title,
                Description = newTask.Description,
                IsCompleted = newTask.IsCompleted,
                CreatedAt = newTask.CreatedAt
            };
        }
    }
}
