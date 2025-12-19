using BackendProjectManagement.DTOs;

namespace BackendProjectManagement.Services
{
    public interface IAuthService
    {
        Task RegisterAsync(RegisterDto dto);
        Task<string> LoginAsync(LoginDto dto);

    }
}
