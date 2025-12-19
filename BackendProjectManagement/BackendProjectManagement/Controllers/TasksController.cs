using BackendProjectManagement.DTOs;
using BackendProjectManagement.Mapper;
using BackendProjectManagement.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackendProjectManagement.Controllers
{
    [Authorize]
    [Route("api/projects/{projectId:guid}/tasks")]
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

        [HttpPatch("{taskId:guid}/complete")]
        public async Task<IActionResult> MarkAsCompleted(
        Guid projectId,
        Guid taskId)
        {
            var success = await _taskService.MarkAsCompletedAsync(taskId);

            if (!success)
                return NotFound("Task not found");

            return NoContent();
        }

        [HttpDelete("{taskId:guid}")]
        public async Task<IActionResult> DeleteTask(Guid projectId,Guid taskId)
        {
            var success = await _taskService.DeleteAsync(taskId);

            if (!success)
                return NotFound("Task not found");

            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask(
    Guid projectId,
    [FromBody] CreateTaskDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var taskEntity = TaskMapper.ToEntity(dto);

            
            taskEntity.ProjectId = projectId;

            var createdTask = await _taskService.CreateAsync(taskEntity);

            var result = TaskMapper.ToDto(createdTask);

            return CreatedAtAction(
                nameof(GetTasksByProject),
                new { projectId = projectId },
                result);
        }


        [HttpPatch("{taskId:guid}")]
        public async Task<IActionResult> UpdateTask(Guid projectId, Guid taskId, [FromBody] UpdateTaskDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var taskEntity = TaskMapper.ToEntity(dto);
            taskEntity.ProjectId = projectId;
            try
            {
                var updatedTask = await _taskService.UpdateAsync(taskId, taskEntity);
                var result = TaskMapper.ToDto(updatedTask);
                return Ok(result);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Task not found");
            }
        }


    }
}
