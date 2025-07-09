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
    public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand, TaskResponseModel>
    {
        private readonly ITaskRepository _taskRepository;

        public UpdateTaskCommandHandler(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<TaskResponseModel> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            var existingTask = await _taskRepository.GetByIdAsync(request.Id);

            if (existingTask == null)
                throw new KeyNotFoundException($"Task with Id {request.Id} not found.");

            existingTask.Title = request.Title;
            existingTask.Description = request.Description;
            existingTask.IsCompleted = request.IsCompleted;
            existingTask.UpdatedAt = DateTime.UtcNow;

            await _taskRepository.UpdateAsync(existingTask);

            return new TaskResponseModel
            {
                Id = existingTask.Id,
                Title = existingTask.Title,
                Description = existingTask.Description,
                IsCompleted = existingTask.IsCompleted,
                CreatedAt = existingTask.CreatedAt,
                UpdatedAt = existingTask.UpdatedAt
            };
        }
    }
}
