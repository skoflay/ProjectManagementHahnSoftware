using BackendProjectManagement.Application.DTOs.TaskDTOs;
using BackendProjectManagement.Domain.Entities.Models;

namespace BackendProjectManagement.Application.Mapper
{
    public static class TaskMapper
    {

        public static TaskItem ToEntity(CreateTaskDto dto)
        {
            return new TaskItem
            {
                Id = Guid.NewGuid(),
                Title = dto.Title,
                Description = dto.Description,
                DueDate = dto.DueDate,
                ProjectId = dto.ProjectId
            };
        }


        public static TaskDto ToDto(TaskItem entity)
        {
            return new TaskDto
            {
                Id= entity.Id,
                Title = entity.Title,
                Description = entity.Description,
                DueDate = entity.DueDate,
                IsCompleted = entity.IsCompleted
            };
        }

        public static List<TaskDto> ToDtoList(IEnumerable<TaskItem> tasks)
        {
            return tasks.Select(ToDto).ToList();
        }
        public static TaskItem ToEntity(UpdateTaskDto dto)
        {
            return new TaskItem
            {
                Title = dto.Title,
                Description = dto.Description,
                DueDate = dto.DueDate ?? DateTime.Now,
                IsCompleted = dto.IsCompleted ?? false
            };
        }



    }
}
