using BackendProjectManagement.Data;
using BackendProjectManagement.DTOs;
using BackendProjectManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendProjectManagement.Repositories
{
    public class ProjectRepository :IProjectRepository
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
        public async Task<List<Project>> GetAllAsync()
        {
            return await _context.Projects.ToListAsync();
        }
        public async Task<bool> UpdateAsync(Project project)
        {
            _context.Projects.Update(project);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null)
                return false;

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Project?> GetByIdAsync(Guid id)
        {
            return await _context.Projects.FindAsync(id);
        }

        public async Task<PagedResultDto<Project>> GetPagedAsync(int page, int pageSize)
        {
            var query = _context.Projects.AsQueryable();

            var totalItems = await query.CountAsync();

            var items = await query
                .OrderByDescending(p => p.CreatedAt) 
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResultDto<Project>
            {
                Items = items,
                Page = page,
                PageSize = pageSize,
                TotalItems = totalItems
            };
        }



    }
}
