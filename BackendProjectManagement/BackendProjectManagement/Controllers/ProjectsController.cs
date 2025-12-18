using BackendProjectManagement.DTOs;
using BackendProjectManagement.Mapper;
using BackendProjectManagement.Models;
using BackendProjectManagement.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackendProjectManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectsController(IProjectService projectService)
        {
            _projectService = projectService;
        }


        [HttpPost]
        public async Task<IActionResult> CreateProject([FromBody] CreateProjectDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Title))
                return BadRequest("Title is required");

            var project = ProjectMapper.ToEntity(dto);  

            var created = await _projectService.CreateAsync(project);
            return CreatedAtAction(nameof(GetProjectById), new { id = created.Id }, created);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProjectById(Guid id)
        {
            var project = await _projectService.GetByIdAsync(id);
            if (project == null)
                return NotFound();
            return Ok(project);
        }


       

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(Guid id, [FromBody] UpdateProjectDto dto)
        {
            var project = await _projectService.GetByIdAsync(id);
            if (project == null)
                return NotFound();

            ProjectMapper.UpdateEntity(project, dto); 

            await _projectService.UpdateAsync(project);
            return NoContent();
        }

        [HttpDelete("{projectId:guid}")]
        public async Task<IActionResult> DeleteProject(Guid projectId)
        {
            var success = await _projectService.DeleteAsync(projectId);

            if (!success)
                return NotFound("Project not found");

            return NoContent();
        }

        [HttpGet("{projectId:guid}/progress")]
        public async Task<IActionResult> GetProgress(Guid projectId)
        {
            var progress = await _projectService.GetProgressAsync(projectId);
            return Ok(progress);
        }

        [HttpGet]
        public async Task<ActionResult<PagedResultDto<ProjectDto>>> GetProjects(
    [FromQuery] int page = 1,
    [FromQuery] int pageSize = 6)
        {
            var result = await _projectService.GetPagedAsync(page, pageSize);

            return Ok(new PagedResultDto<ProjectDto>
            {
                Items = result.Items.Select(ProjectMapper.ToDto),
                Page = result.Page,
                PageSize = result.PageSize,
                TotalItems = result.TotalItems
            });
        }






    }
}
