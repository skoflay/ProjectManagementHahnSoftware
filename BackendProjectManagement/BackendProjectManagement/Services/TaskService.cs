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
    }
}
