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
	public class ToggleTaskCommandHandler : IRequestHandler<ToggleTaskCommand, TaskResponseModel>
	{
		private readonly ITaskRepository _taskRepository;

		public ToggleTaskCommandHandler(ITaskRepository taskRepository)
		{
			_taskRepository = taskRepository;
		}

		public async Task<TaskResponseModel> Handle(ToggleTaskCommand request, CancellationToken cancellationToken)
		{
			var task = await _taskRepository.GetByIdAsync(request.Id);
			if (task == null)
				throw new KeyNotFoundException($"Task with ID {request.Id} not found.");

			task.IsCompleted = !task.IsCompleted;
			task.UpdatedAt = DateTime.UtcNow;

			await _taskRepository.UpdateAsync(task);

			return new TaskResponseModel
			{
				Id = task.Id,
				Title = task.Title,
				Description = task.Description,
				IsCompleted = task.IsCompleted,
				CreatedAt = task.CreatedAt,
				UpdatedAt = task.UpdatedAt
			};
		}
	}
}
