using BackendProjectManagement;
using BackendProjectManagement.DTOs;
using BackendProjectManagement.Models;
using BackendProjectManagement.Repositories;
using BackendProjectManagement.Services;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace BackendProjectManagement.Tests.Services
{
    public class AuthServiceTests
    {
        private readonly Mock<IAuthRepository> _authRepoMock;
        private readonly Mock<ITokenService> _tokenServiceMock;
        private readonly AuthService _authService;

        public AuthServiceTests()
        {
            _authRepoMock = new Mock<IAuthRepository>();
            _tokenServiceMock = new Mock<ITokenService>();
            _authService = new AuthService(_authRepoMock.Object, _tokenServiceMock.Object);
        }

        [Fact]
        public async Task RegisterAsync_EmailAlreadyExists_ThrowsException()
        {
            
            var dto = new RegisterDto { Email = "test@test.com", Password = "password" };
            _authRepoMock.Setup(x => x.EmailExistsAsync(dto.Email)).ReturnsAsync(true);

            
            await Assert.ThrowsAsync<Exception>(() => _authService.RegisterAsync(dto));
        }

        [Fact]
        public async Task RegisterAsync_ValidData_AddsUser()
        {
            
            var dto = new RegisterDto { Email = "test@test.com", Password = "password" };
            _authRepoMock.Setup(x => x.EmailExistsAsync(dto.Email)).ReturnsAsync(false);

            
            await _authService.RegisterAsync(dto);

           
            _authRepoMock.Verify(x => x.AddUserAsync(It.Is<User>(u => u.Email == dto.Email)), Times.Once);
        }

        [Fact]
        public async Task LoginAsync_InvalidCredentials_ThrowsException()
        {
          
            var dto = new LoginDto { Email = "test@test.com", Password = "wrongpass" };
            _authRepoMock.Setup(x => x.GetByEmailAsync(dto.Email)).ReturnsAsync((User)null);

            
            await Assert.ThrowsAsync<Exception>(() => _authService.LoginAsync(dto));
        }

        [Fact]
        public async Task LoginAsync_ValidCredentials_ReturnsToken()
        {
           
            var dto = new LoginDto { Email = "test@test.com", Password = "password" };
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var user = new User { Id = Guid.NewGuid(), Email = dto.Email, Password = hashedPassword };
            _authRepoMock.Setup(x => x.GetByEmailAsync(dto.Email)).ReturnsAsync(user);
            _tokenServiceMock.Setup(x => x.GenerateToken(user)).Returns("mock-token");

          
            var token = await _authService.LoginAsync(dto);

            
            Assert.Equal("mock-token", token);
        }

        [Fact]
        public async Task GetUserById_UserNotFound_ThrowsException()
        {
            var userId = Guid.NewGuid();
            _authRepoMock.Setup(x => x.GetByIdAsync(userId)).ReturnsAsync((User)null);

            await Assert.ThrowsAsync<ArgumentException>(() => _authService.GetUserById(userId));
        }

        [Fact]
        public async Task GetUserById_UserExists_ReturnsUser()
        {
            var userId = Guid.NewGuid();
            var user = new User { Id = userId, Email = "test@test.com" };
            _authRepoMock.Setup(x => x.GetByIdAsync(userId)).ReturnsAsync(user);

            var result = await _authService.GetUserById(userId);

            Assert.Equal(user, result);
        }





    }
}
