using BackendProjectManagement.Application.ServiceInterfaces;
using BackendProjectManagement.Domain.Entities.Models;
using BackendProjectManagement.DTOs;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace BackendProjectManagement.Tests.Controllers
{
    public class AuthControllerTests
    {
        private readonly Mock<IAuthService> _authServiceMock;
        private readonly AuthController _authController;

        public AuthControllerTests()
        {
            _authServiceMock = new Mock<IAuthService>();
            _authController = new AuthController(_authServiceMock.Object);
        }

        [Fact]
        public async Task Register_ShouldReturnOk_WhenRegistrationSuccessful()
        {
            // Arrange
            var dto = new RegisterDto
            {
                Email = "test@test.com",
                Password = "password"
            };

            _authServiceMock.Setup(s => s.RegisterAsync(dto))
                            .Returns(Task.CompletedTask);

            // Act
            var result = await _authController.Register(dto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("User registered", okResult.Value);
        }

        [Fact]
        public async Task Login_ShouldReturnOkWithToken_WhenCredentialsValid()
        {
            // Arrange
            var dto = new LoginDto
            {
                Email = "test@test.com",
                Password = "password"
            };

            var user = new User { Email = dto.Email };
            var token = "dummy-token";

            _authServiceMock.Setup(s => s.LoginAsync(dto))
                            .ReturnsAsync(token);
            _authServiceMock.Setup(s => s.GetByEmailAsync(dto.Email))
                            .ReturnsAsync(user);

            // Act
            var result = await _authController.Login(dto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            dynamic response = okResult.Value!;
            Assert.Equal(token, response.token);
            Assert.Equal(dto.Email, response.email);
        }

        [Fact]
        public async Task Login_ShouldReturnNotFound_WhenUserDoesNotExist()
        {
            // Arrange
            var dto = new LoginDto
            {
                Email = "nonexistent@test.com",
                Password = "password"
            };

            _authServiceMock.Setup(s => s.LoginAsync(dto))
                            .ReturnsAsync("dummy-token");
            _authServiceMock.Setup(s => s.GetByEmailAsync(dto.Email))
                            .ReturnsAsync((User?)null);

            // Act
            var result = await _authController.Login(dto);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
