using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendorManagementProject.Models;
using VendorManagementProject.Repository.Interfaces;
using VendorManagementProject.Services.Class;

namespace VendorManagementProject.Tests.ServicesTest
{
    public class AuthServiceTests
    {
        private readonly Mock<IAuthRepository> _mockAuthRepository;
        private readonly AuthService _authService;

        public AuthServiceTests()
        {
            _mockAuthRepository = new Mock<IAuthRepository>();
            _authService = new AuthService(_mockAuthRepository.Object);
        }

        [Fact]
        public async Task Register_WithValidUser_ReturnsToken()
        {
            // Arrange
            var user = new VendorUser
            {
                UserID = "john.doe",
                Password = "Password@123"
            };

            _mockAuthRepository.Setup(repo => repo.Register(user)).ReturnsAsync("fakeToken");

            // Act
            var result = await _authService.Register(user);

            // Assert
            result.Should().Be("fakeToken");
            _mockAuthRepository.Verify(repo => repo.Register(user), Times.Once);
        }

        [Fact]
        public async Task Register_WithExistingUser_ThrowsException()
        {
            // Arrange
            var user = new VendorUser
            {
                UserID = "existingUser",
                Password = "Password@123"
            };

            _mockAuthRepository.Setup(repo => repo.Register(user)).ThrowsAsync(new Exception("User already exists."));

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _authService.Register(user));
        }

        [Fact]
        public async Task Login_WithValidCredentials_ReturnsToken()
        {
            // Arrange
            _mockAuthRepository.Setup(repo => repo.Login("john.doe", "Password@123")).ReturnsAsync("fakeToken");

            // Act
            var result = await _authService.Login("john.doe", "Password@123");

            // Assert
            result.Should().Be("fakeToken");
            _mockAuthRepository.Verify(repo => repo.Login("john.doe", "Password@123"), Times.Once);
        }

        [Fact]
        public async Task Login_WithInvalidCredentials_ThrowsException()
        {
            // Arrange
            _mockAuthRepository.Setup(repo => repo.Login("invalidUser", "WrongPassword")).ThrowsAsync(new Exception("Invalid credentials."));

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _authService.Login("invalidUser", "WrongPassword"));
        }
    }
}