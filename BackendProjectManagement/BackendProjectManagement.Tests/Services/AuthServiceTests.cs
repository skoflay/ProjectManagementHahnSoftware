using BackendProjectManagement;
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
            _authService = new IAuthService(_authRepoMock.Object, _tokenServiceMock.Object);
        }

        // Tests will go here
    }
}
