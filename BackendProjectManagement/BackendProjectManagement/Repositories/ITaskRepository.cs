using BackendProjectManagement.Models;

namespace BackendProjectManagement.Repositories
{
    public interface ITaskRepository
    {
        Task<TaskItem?> GetByIdAsync(Guid id);
        Task<List<TaskItem>> GetByProjectIdAsync(int projectId);
        Task AddAsync(TaskItem task);
        Task UpdateAsync(TaskItem task);
        Task DeleteAsync(TaskItem task);
    }

}
