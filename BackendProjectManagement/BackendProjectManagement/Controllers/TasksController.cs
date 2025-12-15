using BackendProjectManagement.DTOs;
using BackendProjectManagement.Mapper;
using BackendProjectManagement.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackendProjectManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTasksByProject(Guid projectId)
        {
            var tasks = await _taskService.GetByProjectAsync(projectId);

            if(tasks == null || !tasks.Any())
                return NotFound("No tasks found for the specified project");

            var result = TaskMapper.ToDtoList(tasks);
            return Ok(result);
        }

        [HttpPatch]
        public async Task<IActionResult> MarkAsCompleted(
        Guid projectId,
        Guid taskId)
        {
            var success = await _taskService.MarkAsCompletedAsync(taskId);

            if (!success)
                return NotFound("Task not found");

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTask(Guid projectId,Guid taskId)
        {
            var success = await _taskService.DeleteAsync(taskId);

            if (!success)
                return NotFound("Task not found");

            return NoContent();
        }



    }
}
