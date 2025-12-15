using BackendProjectManagement.Models;

namespace BackendProjectManagement.Services
{
    public interface IProjectService
    {
        Task<Project> CreateAsync(Project project);
        Task<List<Project>> GetAllAsync();
        Task<bool> UpdateAsync(Project project);
        Task<bool> DeleteAsync(Guid id);
        Task<Project?> GetByIdAsync(Guid id);

    }
}
