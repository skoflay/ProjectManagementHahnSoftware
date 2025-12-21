using BackendProjectManagement.Application.DTOs.AuthDTOs;
using BackendProjectManagement.Application.ServiceInterfaces;
using BackendProjectManagement.Infrastructure.Data;
using BackendProjectManagement.Domain.Entities.Interfaces.RepositoryInterfaces;
using BackendProjectManagement.Domain.Entities.Models;
using BCrypt.Net;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class AuthService : IAuthService
{
    private readonly IAuthRepository _authRepository;
    private readonly ITokenService _tokenService;

    public AuthService(IAuthRepository authRepository,ITokenService tokenService)
    {
        _authRepository = authRepository;
        _tokenService = tokenService;
    }

    public async Task RegisterAsync(RegisterDto dto)
    {
        if (await _authRepository.EmailExistsAsync(dto.Email))
            throw new Exception("Email already exists");

        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = dto.Email,
            Password = BCrypt.Net.BCrypt.HashPassword(dto.Password)
        };

        await _authRepository.AddUserAsync(user);
    }

    public async Task<string> LoginAsync(LoginDto dto)
    {
        var user = await _authRepository.GetByEmailAsync(dto.Email);

        if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
            throw new Exception("Invalid credentials");

        return _tokenService.GenerateToken(user);
    }

    public async Task<User?> GetUserById(Guid id)
    {
        var user = await _authRepository.GetByIdAsync(id);

        if (user == null)
            throw new ArgumentException("There is no user with this Id");

        return user;
    }
    public async Task<User> GetByEmailAsync(string email)
    {
        var user = await _authRepository.GetByEmailAsync(email);
        if (user == null) throw new Exception("User not found");
        return user;
    }




}
