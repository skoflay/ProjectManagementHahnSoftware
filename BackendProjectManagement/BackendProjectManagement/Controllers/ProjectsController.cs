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

            var project = new Project
            {
                Title = dto.Title,
                Description = dto.Description
            };

            var created = await _projectService.CreateAsync(project);
            return CreatedAtAction(nameof(GetProjectById), new { id = created.Id }, created);
        }


        [HttpGet]
        public async Task<IActionResult> GetProjects()
        {
            var projects = await _projectService.GetAllAsync();
            return Ok(projects);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(Guid id, [FromBody] UpdateProjectDto dto)
        {
            var project = await _projectService.GetByIdAsync(id);
            if (project == null)
                return NotFound();

            project.Title = dto.Title;
            project.Description = dto.Description;

            await _projectService.UpdateAsync(project);
            return NoContent();
        }





    }
}
