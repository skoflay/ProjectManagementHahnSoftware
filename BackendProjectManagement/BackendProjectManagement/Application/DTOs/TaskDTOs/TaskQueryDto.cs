namespace BackendProjectManagement.DTOs.TaskDTOs
{
    namespace BackendProjectManagement.DTOs.TaskDTOs
    {
        public class TaskQueryDto
        {
            public int Page { get; set; } = 1;
            public int PageSize { get; set; } = 10;
            public string? Search { get; set; }
        }
    }

}
