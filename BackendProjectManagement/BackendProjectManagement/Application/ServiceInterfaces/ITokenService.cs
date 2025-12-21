using BackendProjectManagement.Domain.Entities.Models;
using System.Security.Claims;

namespace BackendProjectManagement.Application.ServiceInterfaces
{
    public interface ITokenService
    {
        string GenerateToken(User user);
        ClaimsPrincipal? ValidateToken(string token);
    }
}
