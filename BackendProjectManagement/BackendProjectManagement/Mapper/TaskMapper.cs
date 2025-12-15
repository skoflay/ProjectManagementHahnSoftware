using BackendProjectManagement.DTOs;
using BackendProjectManagement.Models;

namespace BackendProjectManagement.Mapper
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



    }
}
