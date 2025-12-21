using System.ComponentModel.DataAnnotations;

namespace BackendProjectManagement.Application.DTOs.TaskDTOs
{
    public class CreateTaskDto
    {
        [Required]
        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        public DateTime? DueDate { get; set; }

        [Required]
        public Guid ProjectId { get; set; }
    }
}
