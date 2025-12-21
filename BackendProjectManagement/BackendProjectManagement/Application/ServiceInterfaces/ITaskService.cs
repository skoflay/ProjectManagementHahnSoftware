using BackendProjectManagement.Application.DTOs.ProjectDTOs;
using BackendProjectManagement.Domain.Entities.Models;
using BackendProjectManagement.DTOs.TaskDTOs.BackendProjectManagement.DTOs.TaskDTOs;

namespace BackendProjectManagement.Application.ServiceInterfaces
{
    public interface ITaskService
    {
        Task<TaskItem> CreateAsync(TaskItem task);
        Task<List<TaskItem>> GetByProjectAsync(Guid projectId);
        Task<bool> MarkAsCompletedAsync(Guid taskId);
        Task<bool> DeleteAsync(Guid taskId);
        Task<TaskItem> UpdateAsync(Guid taskId, TaskItem updatedTask);
        Task<PagedResultDto<TaskItem>> GetPagedByProjectAsync(Guid projectId, TaskQueryDto dto);

    }

}
