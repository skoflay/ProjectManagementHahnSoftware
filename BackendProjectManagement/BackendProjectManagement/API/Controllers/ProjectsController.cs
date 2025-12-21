using BackendProjectManagement.Application.DTOs;
using BackendProjectManagement.Application.DTOs.ProjectDTOs;
using BackendProjectManagement.Application.Mapper;
using BackendProjectManagement.Application.ServiceInterfaces;
using BackendProjectManagement.Domain.Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BackendProjectManagement.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectsController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        private Guid GetUserId()
        {
            
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                throw new Exception("UserId claim not found");
            return Guid.Parse(userIdClaim.Value);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProject([FromBody] CreateProjectDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Title))
                return BadRequest("Title is required");

            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var project = ProjectMapper.ToEntity(dto);
            project.UserId = userId; 

            var created = await _projectService.CreateAsync(project);
            return CreatedAtAction(nameof(GetProjectById), new { id = created.Id }, created);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProjectById(Guid id)
        {
            var project = await _projectService.GetByIdAsync(id, GetUserId());
            if (project == null)
                return NotFound();
            return Ok(project);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(Guid id, [FromBody] UpdateProjectDto dto)
        {
            var project = await _projectService.GetByIdAsync(id, GetUserId());
            if (project == null)
                return NotFound();

            ProjectMapper.UpdateEntity(project, dto);
            await _projectService.UpdateAsync(project, GetUserId());
            return NoContent();
        }

        [HttpDelete("{projectId:guid}")]
        public async Task<IActionResult> DeleteProject(Guid projectId)
        {
            var success = await _projectService.DeleteAsync(projectId, GetUserId());
            if (!success)
                return NotFound("Project not found");

            return NoContent();
        }

        [HttpGet("{projectId:guid}/progress")]
        public async Task<IActionResult> GetProgress(Guid projectId)
        {
            
            var project = await _projectService.GetByIdAsync(projectId, GetUserId());
            if (project == null)
                return NotFound();

            var progress = await _projectService.GetProgressAsync(projectId);
            return Ok(progress);
        }

        [HttpGet]
        public async Task<ActionResult<PagedResultDto<ProjectDto>>> GetProjects(
    [FromQuery] ProjectQueryDto query)
        {
            var userId = GetUserId();

            var result = await _projectService.GetPagedAsync(userId, query);

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
