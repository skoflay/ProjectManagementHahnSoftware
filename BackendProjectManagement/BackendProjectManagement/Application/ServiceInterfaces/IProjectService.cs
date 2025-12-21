using BackendProjectManagement.Application.DTOs;
using BackendProjectManagement.Application.DTOs.ProjectDTOs;
using BackendProjectManagement.Domain.Entities.Models;

namespace BackendProjectManagement.Application.ServiceInterfaces
{
    public interface IProjectService
    {
        Task<Project> CreateAsync(Project project);
        Task<List<Project>> GetAllAsync(Guid userId);
        Task<bool> UpdateAsync(Project project, Guid userId);
        Task<bool> DeleteAsync(Guid id, Guid userId);
        Task<Project?> GetByIdAsync(Guid id, Guid userId);
        Task<PagedResultDto<Project>> GetPagedAsync(
    Guid userId,
    ProjectQueryDto query
);

        Task<ProjectProgressDto> GetProgressAsync(Guid projectId);

    }
}
