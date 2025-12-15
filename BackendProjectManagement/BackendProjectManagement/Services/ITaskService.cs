using BackendProjectManagement.Models;

namespace BackendProjectManagement.Services
{
    public interface ITaskService
    {
        Task<TaskItem> CreateAsync(TaskItem task);
        Task<List<TaskItem>> GetByProjectAsync(int projectId);
        Task<bool> MarkAsCompletedAsync(Guid taskId);
        Task<bool> DeleteAsync(int taskId);
    }

}
