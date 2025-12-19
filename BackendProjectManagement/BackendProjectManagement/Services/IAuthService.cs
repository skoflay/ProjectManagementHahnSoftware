using BackendProjectManagement.DTOs;
using BackendProjectManagement.Models;

namespace BackendProjectManagement.Services
{
    public interface IAuthService
    {
        Task RegisterAsync(RegisterDto dto);
        Task<string> LoginAsync(LoginDto dto);
        Task<User?> GetUserById(Guid id);
        Task<User> GetByEmailAsync(string email);
    }
}
