using BackendProjectManagement.DTOs;
using BackendProjectManagement.Models;

namespace BackendProjectManagement.Repositories
{
    public interface IProjectRepository
    {
        Task<Project> AddAsync(Project project);
        Task<List<Project>> GetAllAsync();
        Task<bool> UpdateAsync(Project project);
        Task<bool> DeleteAsync(Guid id);
        Task<Project?> GetByIdAsync(Guid id);
        Task<PagedResultDto<Project>> GetPagedAsync(int page, int pageSize);
    }

}
