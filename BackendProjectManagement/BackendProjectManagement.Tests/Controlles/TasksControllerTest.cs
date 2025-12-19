using BackendProjectManagement.Controllers;
using BackendProjectManagement.DTOs;
using BackendProjectManagement.Models;
using BackendProjectManagement.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BackendProjectManagement.Tests.Controllers
{
    public class TasksControllerTests
    {
        private readonly Mock<ITaskService> _taskServiceMock;
        private readonly TasksController _controller;
        private readonly Guid _projectId = Guid.NewGuid();
        private readonly Guid _taskId = Guid.NewGuid();

        public TasksControllerTests()
        {
            _taskServiceMock = new Mock<ITaskService>();
            _controller = new TasksController(_taskServiceMock.Object);

            // Mock HttpContext si nécessaire
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };
        }

        [Fact]
        public async Task GetTasksByProject_ShouldReturnOk_WhenTasksExist()
        {
            var tasks = new List<TaskItem>
            {
                new TaskItem { Id = _taskId, Title = "Task1", ProjectId = _projectId }
            };

            _taskServiceMock.Setup(s => s.GetByProjectAsync(_projectId))
                .ReturnsAsync(tasks);

            var result = await _controller.GetTasksByProject(_projectId);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedTasks = Assert.IsAssignableFrom<IEnumerable<TaskDto>>(okResult.Value);
            Assert.Single(returnedTasks);
        }

        [Fact]
        public async Task GetTasksByProject_ShouldReturnNotFound_WhenNoTasks()
        {
            _taskServiceMock.Setup(s => s.GetByProjectAsync(_projectId))
                .ReturnsAsync(new List<TaskItem>());

            var result = await _controller.GetTasksByProject(_projectId);

            var notFound = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("No tasks found for the specified project", notFound.Value);
        }

        [Fact]
        public async Task MarkAsCompleted_ShouldReturnNoContent_WhenSuccess()
        {
            _taskServiceMock.Setup(s => s.MarkAsCompletedAsync(_taskId))
                .ReturnsAsync(true);

            var result = await _controller.MarkAsCompleted(_projectId, _taskId);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task MarkAsCompleted_ShouldReturnNotFound_WhenTaskDoesNotExist()
        {
            _taskServiceMock.Setup(s => s.MarkAsCompletedAsync(_taskId))
                .ReturnsAsync(false);

            var result = await _controller.MarkAsCompleted(_projectId, _taskId);

            var notFound = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Task not found", notFound.Value);
        }

        [Fact]
        public async Task DeleteTask_ShouldReturnNoContent_WhenSuccess()
        {
            _taskServiceMock.Setup(s => s.DeleteAsync(_taskId))
                .ReturnsAsync(true);

            var result = await _controller.DeleteTask(_projectId, _taskId);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteTask_ShouldReturnNotFound_WhenTaskDoesNotExist()
        {
            _taskServiceMock.Setup(s => s.DeleteAsync(_taskId))
                .ReturnsAsync(false);

            var result = await _controller.DeleteTask(_projectId, _taskId);

            var notFound = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Task not found", notFound.Value);
        }

        [Fact]
        public async Task CreateTask_ShouldReturnCreated_WhenValidDto()
        {
            var dto = new CreateTaskDto { Title = "New Task" };
            var task = new TaskItem { Id = _taskId, Title = dto.Title, ProjectId = _projectId };

            _taskServiceMock.Setup(s => s.CreateAsync(It.IsAny<TaskItem>()))
                .ReturnsAsync(task);

            var result = await _controller.CreateTask(_projectId, dto);

            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnedTask = Assert.IsType<TaskDto>(createdResult.Value);
            Assert.Equal(dto.Title, returnedTask.Title);
        }

        [Fact]
        public async Task UpdateTask_ShouldReturnOk_WhenTaskExists()
        {
            var dto = new UpdateTaskDto { Title = "Updated Task" };
            var task = new TaskItem { Id = _taskId, Title = dto.Title, ProjectId = _projectId };

            _taskServiceMock.Setup(s => s.UpdateAsync(_taskId, It.IsAny<TaskItem>()))
                .ReturnsAsync(task);

            var result = await _controller.UpdateTask(_projectId, _taskId, dto);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedTask = Assert.IsType<TaskDto>(okResult.Value);
            Assert.Equal(dto.Title, returnedTask.Title);
        }

        [Fact]
        public async Task UpdateTask_ShouldReturnNotFound_WhenTaskDoesNotExist()
        {
            var dto = new UpdateTaskDto { Title = "Updated Task" };

            _taskServiceMock.Setup(s => s.UpdateAsync(_taskId, It.IsAny<TaskItem>()))
                .ThrowsAsync(new KeyNotFoundException());

            var result = await _controller.UpdateTask(_projectId, _taskId, dto);

            var notFound = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Task not found", notFound.Value);
        }
    }
}
