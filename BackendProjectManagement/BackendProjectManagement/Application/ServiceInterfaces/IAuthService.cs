using BackendProjectManagement.Application.DTOs.AuthDTOs;
using BackendProjectManagement.Domain.Entities.Models;

namespace BackendProjectManagement.Application.ServiceInterfaces
{
    public interface IAuthService
    {
        Task RegisterAsync(RegisterDto dto);
        Task<string> LoginAsync(LoginDto dto);
        Task<User?> GetUserById(Guid id);
        Task<User> GetByEmailAsync(string email);
    }
}
