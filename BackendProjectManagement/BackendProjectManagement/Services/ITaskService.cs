using BackendProjectManagement.Models;

namespace BackendProjectManagement.Services
{
    public interface ITaskService
    {
        Task<TaskItem> CreateAsync(TaskItem task);
        Task<List<TaskItem>> GetByProjectAsync(Guid projectId);
        Task<bool> MarkAsCompletedAsync(Guid taskId);
        Task<bool> DeleteAsync(Guid taskId);
        Task<TaskItem> UpdateAsync(Guid taskId, TaskItem updatedTask);
    }

}
