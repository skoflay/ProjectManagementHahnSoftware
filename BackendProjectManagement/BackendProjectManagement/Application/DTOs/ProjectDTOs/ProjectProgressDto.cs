namespace BackendProjectManagement.Application.DTOs.ProjectDTOs
{
    public class ProjectProgressDto
    {
        public int TotalTasks { get; set; }
        public int CompletedTasks { get; set; }
        public int ProgressPercentage { get; set; }
        public bool IsCompleted { get; set; }
    }
}
