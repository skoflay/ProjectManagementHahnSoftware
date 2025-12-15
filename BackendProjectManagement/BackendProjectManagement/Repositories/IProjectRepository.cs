using BackendProjectManagement.Models;

namespace BackendProjectManagement.Repositories
{
    public interface IProjectRepository
    {
        Task<Project> AddAsync(Project project);
        Task<List<Project>> GetAllAsync();
        Task<Project?> GetByIdAsync(Guid id);
        Task<bool> UpdateAsync(Project project);
        Task<bool> DeleteAsync(Guid id);
    }

}
