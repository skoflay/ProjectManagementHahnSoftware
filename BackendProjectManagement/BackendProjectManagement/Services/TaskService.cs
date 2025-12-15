using BackendProjectManagement.Models;
using BackendProjectManagement.Repositories;

namespace BackendProjectManagement.Services
{
    public class TaskService :ITaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<TaskItem> CreateAsync(TaskItem task)
        {
            task.IsCompleted = false;
            await _taskRepository.AddAsync(task);
            return task;
        }
    }
}
