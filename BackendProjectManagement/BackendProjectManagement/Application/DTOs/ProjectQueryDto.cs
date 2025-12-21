namespace BackendProjectManagement.Application.DTOs
{
    public class ProjectQueryDto
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 6;
        public string? Search { get; set; }
    }

}
