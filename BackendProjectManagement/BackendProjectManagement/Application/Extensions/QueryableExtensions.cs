using BackendProjectManagement.Application.DTOs.ProjectDTOs;
using Microsoft.EntityFrameworkCore;

namespace BackendProjectManagement.Application.Extensions
{
    public static class QueryableExtensions
    {
        public static async Task<PagedResultDto<T>> ToPagedResultAsync<T>(
            this IQueryable<T> query,
            int page,
            int pageSize)
        {
            var totalItems = await query.CountAsync();

            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResultDto<T>
            {
                Items = items,
                Page = page,
                PageSize = pageSize,
                TotalItems = totalItems
            };
        }
    }

}
