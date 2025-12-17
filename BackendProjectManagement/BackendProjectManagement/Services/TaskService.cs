using BackendProjectManagement.DTOs;
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

        public async Task<List<TaskItem>> GetByProjectAsync(Guid projectId)
        {
            return await _taskRepository.GetByProjectIdAsync(projectId);
        }

        public async Task<bool> MarkAsCompletedAsync(Guid taskId)
        {
            var task = await _taskRepository.GetByIdAsync(taskId);
            if (task == null) return false;

            task.IsCompleted = true;
            await _taskRepository.UpdateAsync(task);
            return true;
        }

        public async Task<bool> DeleteAsync(Guid taskId)
        {
            var task = await _taskRepository.GetByIdAsync(taskId);
            if (task == null) return false;

            await _taskRepository.DeleteAsync(task);
            return true;
        }

        public async Task<TaskItem> UpdateAsync(Guid taskId, TaskItem updatedTask)
        {
            var existingTask = await _taskRepository.GetByIdAsync(taskId);
            if (existingTask == null)
                throw new KeyNotFoundException("Task not found");

            existingTask.Title = updatedTask.Title;
            existingTask.Description = updatedTask.Description;
            existingTask.DueDate = updatedTask.DueDate;
            existingTask.IsCompleted = updatedTask.IsCompleted;

            return await _taskRepository.UpdateAsync(existingTask);
        }


       
        




    }
}
