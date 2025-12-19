using BackendProjectManagement.DTOs;
using BackendProjectManagement.Models;
using BackendProjectManagement.Repositories;
using BackendProjectManagement.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BackendProjectManagement.Tests.Services
{
    public class ProjectServiceTests
    {
        private readonly Mock<IProjectRepository> _projectRepoMock;
        private readonly Mock<ITaskRepository> _taskRepoMock;
        private readonly ProjectService _projectService;

        public ProjectServiceTests()
        {
            _projectRepoMock = new Mock<IProjectRepository>();
            _taskRepoMock = new Mock<ITaskRepository>();
            _projectService = new ProjectService(_projectRepoMock.Object, _taskRepoMock.Object);
        }

        [Fact]
        public async Task CreateAsync_ShouldAssignIdAndCreatedAt_AndReturnProject()
        {
           
            var project = new Project { Title = "Test Project" };
            _projectRepoMock.Setup(r => r.AddAsync(It.IsAny<Project>())).ReturnsAsync((Project p) => p);

           
            var result = await _projectService.CreateAsync(project);

            
            Assert.NotEqual(Guid.Empty, result.Id);
            Assert.True(result.CreatedAt <= DateTime.UtcNow);
            Assert.Equal(project.Title, result.Title);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnProjects()
        {
            var userId = Guid.NewGuid();
            var projects = new List<Project> { new Project { Id = Guid.NewGuid(), Title = "P1" } };
            _projectRepoMock.Setup(r => r.GetAllAsync(userId)).ReturnsAsync(projects);

            var result = await _projectService.GetAllAsync(userId);

            Assert.Equal(projects.Count, result.Count);
            Assert.Equal(projects.First().Title, result.First().Title);
        }

        [Fact]
        public async Task UpdateAsync_ShouldReturnTrueIfSuccessful()
        {
            var project = new Project { Id = Guid.NewGuid(), Title = "P1" };
            _projectRepoMock.Setup(r => r.UpdateAsync(project)).ReturnsAsync(true);

            var result = await _projectService.UpdateAsync(project, Guid.NewGuid());

            Assert.True(result);
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnTrueIfSuccessful()
        {
            var id = Guid.NewGuid();
            _projectRepoMock.Setup(r => r.DeleteAsync(id, It.IsAny<Guid>())).ReturnsAsync(true);

            var result = await _projectService.DeleteAsync(id, Guid.NewGuid());

            Assert.True(result);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnProjectIfExists()
        {
            var project = new Project { Id = Guid.NewGuid(), Title = "P1" };
            _projectRepoMock.Setup(r => r.GetByIdAsync(project.Id, It.IsAny<Guid>())).ReturnsAsync(project);

            var result = await _projectService.GetByIdAsync(project.Id, Guid.NewGuid());

            Assert.Equal(project.Id, result?.Id);
        }

        [Fact]
        public async Task GetProgressAsync_ShouldCalculateCorrectProgress()
        {
            var projectId = Guid.NewGuid();
            var tasks = new List<TaskItem>
            {
                new TaskItem { IsCompleted = true },
                new TaskItem { IsCompleted = false },
                new TaskItem { IsCompleted = true }
            };
            _taskRepoMock.Setup(r => r.GetByProjectIdAsync(projectId)).ReturnsAsync(tasks);

            var result = await _projectService.GetProgressAsync(projectId);

            Assert.Equal(3, result.TotalTasks);
            Assert.Equal(2, result.CompletedTasks);
            Assert.Equal(66, result.ProgressPercentage);
            Assert.False(result.IsCompleted);
        }

        [Fact]
        public async Task GetPagedAsync_ShouldReturnPagedResult()
        {
            var userId = Guid.NewGuid();
            var pagedResult = new PagedResultDto<Project>
            {
                Items = new List<Project> { new Project { Title = "P1" } },
                TotalItems = 1
            };
            _projectRepoMock.Setup(r => r.GetPagedAsync(userId, 1, 10)).ReturnsAsync(pagedResult);

            var result = await _projectService.GetPagedAsync(userId, 1, 10);

            Assert.Single(result.Items);
            Assert.Equal(1, result.TotalItems);
        }
    }
}
