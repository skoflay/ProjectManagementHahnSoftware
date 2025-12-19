using BackendProjectManagement.DTOs;
using BackendProjectManagement.Models;
using BackendProjectManagement.Repositories;

namespace BackendProjectManagement.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _repository;
        private readonly ITaskRepository _taskRepository;

        public ProjectService(IProjectRepository repository, ITaskRepository taskRepository)
        {
            _repository = repository;
            _taskRepository = taskRepository;
        }

        public async Task<Project> CreateAsync(Project project)
        {
            project.Id = Guid.NewGuid();
            project.CreatedAt = DateTime.UtcNow;
           
            return await _repository.AddAsync(project);
        }

        public async Task<List<Project>> GetAllAsync(Guid userId)
        {
            return await _repository.GetAllAsync(userId);
        }

        public async Task<bool> UpdateAsync(Project project, Guid userId)
        {
            return await _repository.UpdateAsync(project);
        }

        public async Task<bool> DeleteAsync(Guid id, Guid userId)
        {
            return await _repository.DeleteAsync(id, userId);
        }

        public async Task<Project?> GetByIdAsync(Guid id, Guid userId)
        {
            return await _repository.GetByIdAsync(id, userId);
        }

        public async Task<ProjectProgressDto> GetProgressAsync(Guid projectId)
        {
            var tasks = await _taskRepository.GetByProjectIdAsync(projectId);

            var total = tasks.Count;
            var completed = tasks.Count(t => t.IsCompleted);

            return new ProjectProgressDto
            {
                TotalTasks = total,
                CompletedTasks = completed,
                ProgressPercentage = total == 0 ? 0 : (completed * 100) / total,
                IsCompleted = total > 0 && completed == total
            };
        }

        public async Task<PagedResultDto<Project>> GetPagedAsync(Guid userId, int page, int pageSize)
        {
            return await _repository.GetPagedAsync(userId, page, pageSize);
        }
    }
}
