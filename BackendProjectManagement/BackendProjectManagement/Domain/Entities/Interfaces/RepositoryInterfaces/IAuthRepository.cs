using BackendProjectManagement.Domain.Entities.Models;

namespace BackendProjectManagement.Domain.Entities.Interfaces.RepositoryInterfaces
{
    public interface IAuthRepository
    {
        Task<bool> EmailExistsAsync(string email);
        Task AddUserAsync(User user);
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByIdAsync(Guid id);
    }
}
