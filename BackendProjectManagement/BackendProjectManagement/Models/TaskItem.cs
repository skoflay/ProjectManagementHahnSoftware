using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendProjectManagement.Models
{
    public class TaskItem
    {
        [Key] 
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [MaxLength(100, ErrorMessage = "Title can't exceed 100 characters")]
        public string Title { get; set; } = null!;

        [MaxLength(500, ErrorMessage = "Description can't exceed 500 characters")]
        public string? Description { get; set; }

        
        public DateTime? DueDate { get; set; }

        public bool IsCompleted { get; set; } = false;

        [Required]
        public Guid ProjectId { get; set; }

        [ForeignKey(nameof(ProjectId))]
        public Project Project { get; set; } = null!;
    }

}
