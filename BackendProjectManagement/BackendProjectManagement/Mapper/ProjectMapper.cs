using BackendProjectManagement.DTOs;
using BackendProjectManagement.Models;

namespace BackendProjectManagement.Mapper
{
    public static class ProjectMapper
    {

        public static Project ToEntity(CreateProjectDto dto)
        {
            return new Project
            {
                Id = Guid.NewGuid(),       
                Title = dto.Title,
                Description = dto.Description,
                CreatedAt = DateTime.UtcNow
            };
        }
        public static ProjectDto ToDto(Project project)
        {
            return new ProjectDto
            {
                Id = project.Id,
                Title = project.Title,
                Description = project.Description,
                CreatedAt = project.CreatedAt,
               
            };
        }

        public static void UpdateEntity(Project project, UpdateProjectDto dto)
        {
            project.Title = dto.Title;
            project.Description = dto.Description;
        }


        public static CreateProjectDto CreateToDto(Project project)
        {
            return new CreateProjectDto
            {
                Title = project.Title,
                Description = project.Description
            };
        }



    }
}
