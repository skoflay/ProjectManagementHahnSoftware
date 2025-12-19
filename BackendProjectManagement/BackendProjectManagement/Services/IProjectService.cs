using BackendProjectManagement.DTOs;
using BackendProjectManagement.Models;

namespace BackendProjectManagement.Services
{
    public interface IProjectService
    {
        Task<Project> CreateAsync(Project project);
        Task<List<Project>> GetAllAsync(Guid userId);
        Task<bool> UpdateAsync(Project project, Guid userId);
        Task<bool> DeleteAsync(Guid id, Guid userId);
        Task<Project?> GetByIdAsync(Guid id, Guid userId);
        Task<PagedResultDto<Project>> GetPagedAsync(Guid userId, int page, int pageSize);
        Task<ProjectProgressDto> GetProgressAsync(Guid projectId);

    }
}
