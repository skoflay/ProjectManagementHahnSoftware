using BackendProjectManagement.DTOs;
using BackendProjectManagement.Models;

namespace BackendProjectManagement.Repositories
{
    public interface IProjectRepository
    {
        Task<Project> AddAsync(Project project);
        Task<List<Project>> GetAllAsync(Guid userId);
        Task<bool> UpdateAsync(Project project);
        Task<bool> DeleteAsync(Guid id, Guid userId);
        Task<Project?> GetByIdAsync(Guid id, Guid userId);
        Task<PagedResultDto<Project>> GetPagedAsync(Guid userId, int page, int pageSize);
    }

}
