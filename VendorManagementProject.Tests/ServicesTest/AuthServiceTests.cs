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
                UserFirstName = "Shins",
                UserLastName = "Mathew",
                UserID = "Shins.Mathew",
                Password = "Shins@123",
                Email = "shinsm@gmail.com",
                Role = "Admin"
            };

            _mockAuthRepository.Setup(repo => repo.Register(user)).ReturnsAsync("Generated_Fake_Token");

            // Act
            var result = await _authService.Register(user);

            // Assert
            result.Should().Be("Generated_Fake_Token");
            _mockAuthRepository.Verify(repo => repo.Register(user), Times.Once);
        }

        [Fact]
        public async Task Register_WithExistingUser_ThrowsException()
        {
            // Arrange
            var user = new VendorUser
            {
                UserFirstName = "Shins",
                UserLastName = "Mathew",
                UserID = "Shins.Mathew",
                Password = "Shins@123",
                Email = "shinsm@gmail.com",
                Role = "Admin"
            };


            _mockAuthRepository.Setup(repo => repo.Register(user)).ThrowsAsync(new Exception("User already exists."));

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _authService.Register(user));
        }

        [Fact]
        public async Task Login_WithValidCredentials_ReturnsToken()
        {
            // Arrange
            _mockAuthRepository.Setup(repo => repo.Login("Shins.Mathew", "Shins@123")).ReturnsAsync("Generated_Fake_Token");

            // Act
            var result = await _authService.Login("Shins.Mathew", "Shins@123");

            // Assert
            result.Should().Be("Generated_Fake_Token");
            _mockAuthRepository.Verify(repo => repo.Login("Shins.Mathew", "Shins@123"), Times.Once);
        }

        [Fact]
        public async Task Login_WithInvalidCredentials_ThrowsException()
        {
            // Arrange
            _mockAuthRepository.Setup(repo => repo.Login("Shinsmathew", "Password")).ThrowsAsync(new Exception("Invalid credentials."));

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _authService.Login("Shinsmathew", "Password"));
        }
    }
}