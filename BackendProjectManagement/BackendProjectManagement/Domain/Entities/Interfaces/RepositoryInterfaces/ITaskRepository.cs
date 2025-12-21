using BackendProjectManagement.Application.DTOs.ProjectDTOs;
using BackendProjectManagement.Domain.Entities.Models;
using BackendProjectManagement.DTOs.TaskDTOs.BackendProjectManagement.DTOs.TaskDTOs;

namespace BackendProjectManagement.Domain.Entities.Interfaces.RepositoryInterfaces
{
    public interface ITaskRepository
    {
        Task<TaskItem?> GetByIdAsync(Guid id);
        Task<List<TaskItem>> GetByProjectIdAsync(Guid projectId);
        Task AddAsync(TaskItem task);
        Task<TaskItem>UpdateAsync(TaskItem task);
        Task DeleteAsync(TaskItem task);
        Task<PagedResultDto<TaskItem>> GetPagedByProjectAsync(
    Guid projectId,
    TaskQueryDto query
);


    }

}
