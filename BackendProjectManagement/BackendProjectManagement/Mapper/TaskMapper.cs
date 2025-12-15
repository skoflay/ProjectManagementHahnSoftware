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


    }
}
