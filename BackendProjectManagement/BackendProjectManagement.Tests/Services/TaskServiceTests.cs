using BackendProjectManagement.Application.Services;
using BackendProjectManagement.Domain.Entities.Interfaces.RepositoryInterfaces;
using BackendProjectManagement.Domain.Entities.Models;
using BackendProjectManagement.DTOs;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace BackendProjectManagement.Tests.Services
{
    public class TaskServiceTests
    {
        private readonly Mock<ITaskRepository> _taskRepoMock;
        private readonly TaskService _taskService;

        public TaskServiceTests()
        {
            _taskRepoMock = new Mock<ITaskRepository>();
            _taskService = new TaskService(_taskRepoMock.Object);
        }

        [Fact]
        public async Task CreateAsync_ShouldSetIsCompletedFalse_AndReturnTask()
        {
            var task = new TaskItem { Title = "Test Task" };
            _taskRepoMock.Setup(r => r.AddAsync(task)).Returns(Task.CompletedTask);

            var result = await _taskService.CreateAsync(task);

            Assert.False(result.IsCompleted);
            Assert.Equal(task.Title, result.Title);
            _taskRepoMock.Verify(r => r.AddAsync(task), Times.Once);
        }

        [Fact]
        public async Task GetByProjectAsync_ShouldReturnTasks()
        {
            var projectId = Guid.NewGuid();
            var tasks = new List<TaskItem>
            {
                new TaskItem { Id = Guid.NewGuid(), Title = "T1" },
                new TaskItem { Id = Guid.NewGuid(), Title = "T2" }
            };
            _taskRepoMock.Setup(r => r.GetByProjectIdAsync(projectId)).ReturnsAsync(tasks);

            var result = await _taskService.GetByProjectAsync(projectId);

            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task MarkAsCompletedAsync_TaskExists_ShouldReturnTrue()
        {
            var taskId = Guid.NewGuid();
            var task = new TaskItem { Id = taskId, IsCompleted = false };
            _taskRepoMock.Setup(r => r.GetByIdAsync(taskId)).ReturnsAsync(task);
            _taskRepoMock.Setup(r => r.UpdateAsync(task)).ReturnsAsync(task);

            var result = await _taskService.MarkAsCompletedAsync(taskId);

            Assert.True(result);
            Assert.True(task.IsCompleted);
            _taskRepoMock.Verify(r => r.UpdateAsync(task), Times.Once);
        }

        [Fact]
        public async Task MarkAsCompletedAsync_TaskDoesNotExist_ShouldReturnFalse()
        {
            var taskId = Guid.NewGuid();
            _taskRepoMock.Setup(r => r.GetByIdAsync(taskId)).ReturnsAsync((TaskItem?)null);

            var result = await _taskService.MarkAsCompletedAsync(taskId);

            Assert.False(result);
        }

        [Fact]
        public async Task DeleteAsync_TaskExists_ShouldReturnTrue()
        {
            var taskId = Guid.NewGuid();
            var task = new TaskItem { Id = taskId };
            _taskRepoMock.Setup(r => r.GetByIdAsync(taskId)).ReturnsAsync(task);
            _taskRepoMock.Setup(r => r.DeleteAsync(task)).Returns(Task.CompletedTask);

            var result = await _taskService.DeleteAsync(taskId);

            Assert.True(result);
            _taskRepoMock.Verify(r => r.DeleteAsync(task), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_TaskDoesNotExist_ShouldReturnFalse()
        {
            var taskId = Guid.NewGuid();
            _taskRepoMock.Setup(r => r.GetByIdAsync(taskId)).ReturnsAsync((TaskItem?)null);

            var result = await _taskService.DeleteAsync(taskId);

            Assert.False(result);
        }

        [Fact]
        public async Task UpdateAsync_TaskExists_ShouldUpdateAndReturnTask()
        {
            var taskId = Guid.NewGuid();
            var existingTask = new TaskItem { Id = taskId, Title = "Old" };
            var updatedTask = new TaskItem { Title = "New", Description = "Desc", IsCompleted = true };

            _taskRepoMock.Setup(r => r.GetByIdAsync(taskId)).ReturnsAsync(existingTask);
            _taskRepoMock.Setup(r => r.UpdateAsync(existingTask)).ReturnsAsync(existingTask);

            var result = await _taskService.UpdateAsync(taskId, updatedTask);

            Assert.Equal("New", result.Title);
            Assert.Equal("Desc", result.Description);
            Assert.True(result.IsCompleted);
            _taskRepoMock.Verify(r => r.UpdateAsync(existingTask), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_TaskDoesNotExist_ShouldThrowKeyNotFoundException()
        {
            var taskId = Guid.NewGuid();
            var updatedTask = new TaskItem { Title = "New" };

            _taskRepoMock.Setup(r => r.GetByIdAsync(taskId)).ReturnsAsync((TaskItem?)null);

            await Assert.ThrowsAsync<KeyNotFoundException>(() => _taskService.UpdateAsync(taskId, updatedTask));
        }
    }
}
