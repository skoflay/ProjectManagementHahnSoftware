using BackendProjectManagement.Models;
using System.Security.Claims;

namespace BackendProjectManagement.Services
{
    public interface ITokenService
    {
        string GenerateToken(User user);
        ClaimsPrincipal? ValidateToken(string token);
    }
}
