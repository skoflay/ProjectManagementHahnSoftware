namespace BackendProjectManagement.Application.DTOs.ProjectDTOs
{
    public class CreateProjectDto
    {
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
    }

}
