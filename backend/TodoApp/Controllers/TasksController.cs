using Core.Application.Command;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TodoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TasksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Create Task
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTaskCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        // Get All Tasks
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllTasksCommand());
            return Ok(result);
        }

        // Update Task
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateTaskCommand command)
        {
            if (id != command.Id) return BadRequest("ID mismatch");

            var result = await _mediator.Send(command);
            return Ok(result);
        }

        // Delete Task
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _mediator.Send(new DeleteTaskCommand(id));
            return Ok(result);
        }

        // Toggle Complete
        [HttpPatch("{id}/toggle")]
        public async Task<IActionResult> Toggle(string id)
        {
            var result = await _mediator.Send(new ToggleTaskCommand { Id = id });
            return Ok(result);
        }
    }
}
