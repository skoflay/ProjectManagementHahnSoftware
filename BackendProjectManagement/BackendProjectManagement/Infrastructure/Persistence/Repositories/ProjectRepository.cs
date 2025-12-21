using BackendProjectManagement.Application.DTOs;
using BackendProjectManagement.Application.DTOs.ProjectDTOs;
using BackendProjectManagement.Application.Extensions;
using BackendProjectManagement.Domain.Entities.Interfaces.RepositoryInterfaces;
using BackendProjectManagement.Domain.Entities.Models;
using BackendProjectManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BackendProjectManagement.Infrastructure.Persistence.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly AppDbContext _context;

        public ProjectRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Project> AddAsync(Project project)
        {
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();
            return project;
        }

        public async Task<List<Project>> GetAllAsync(Guid userId)
        {
            return await _context.Projects
                .Where(p => p.UserId == userId)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();
        }

        public async Task<bool> UpdateAsync(Project project)
        {
            _context.Projects.Update(project);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id, Guid userId)
        {
            var project = await _context.Projects
                .FirstOrDefaultAsync(p => p.Id == id && p.UserId == userId);

            if (project == null)
                return false;

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Project?> GetByIdAsync(Guid id, Guid userId)
        {
            return await _context.Projects
                .FirstOrDefaultAsync(p => p.Id == id && p.UserId == userId);
        }

        public async Task<PagedResultDto<Project>> GetPagedAsync(Guid userId, ProjectQueryDto dto)
        {
            
            var query = _context.Projects.Where(p => p.UserId == userId);

            if (!string.IsNullOrWhiteSpace(dto.Search))
                query = query.Where(p => p.Title.Contains(dto.Search));

            return await query
                .OrderByDescending(p => p.CreatedAt)
                .ToPagedResultAsync(dto.Page, dto.PageSize);
        }

    }
}
