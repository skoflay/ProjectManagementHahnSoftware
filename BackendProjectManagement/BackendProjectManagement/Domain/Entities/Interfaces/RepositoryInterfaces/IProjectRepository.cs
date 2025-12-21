using BackendProjectManagement.Application.DTOs;
using BackendProjectManagement.Application.DTOs.ProjectDTOs;
using BackendProjectManagement.Domain.Entities.Models;

namespace BackendProjectManagement.Domain.Entities.Interfaces.RepositoryInterfaces
{
    public interface IProjectRepository
    {
        Task<Project> AddAsync(Project project);
        Task<List<Project>> GetAllAsync(Guid userId);
        Task<bool> UpdateAsync(Project project);
        Task<bool> DeleteAsync(Guid id, Guid userId);
        Task<Project?> GetByIdAsync(Guid id, Guid userId);
        Task<PagedResultDto<Project>> GetPagedAsync(Guid userId, ProjectQueryDto query);

    }

}
