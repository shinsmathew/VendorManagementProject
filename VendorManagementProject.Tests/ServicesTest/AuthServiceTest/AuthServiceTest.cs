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
using VendorManagementProject.Services.Interface;

namespace VendorManagementProject.Tests.ServicesTest.AuthServiceTest
{
    public class AuthServiceTest
    {
        private readonly Mock<IAuthRepository> _mockAuthRepository;
        private readonly AuthService _authService;

        public AuthServiceTest()
        {
            _mockAuthRepository = new Mock<IAuthRepository>();
            _authService = new AuthService(_mockAuthRepository.Object);
        }

        [Fact]
        public async Task Register_ValidUser_ReturnsToken()
        {
            // Arrange
            var user = new VendorUser
            {

                UserFirstName = "Shins",
                UserLastName = "Mathew",
                UserID = "testUser",
                Password = "Test@1234",
                Email = "test@example.com",
                Role = "User"
            };
            
            _mockAuthRepository.Setup(repo => repo.Register(user)).ReturnsAsync("fakeToken");
            
            // Act
            var result = await _authService.Register(user);

            // Assert
            result.Should().Be("fakeToken");
        }

        [Fact]
        public async Task Login_ValidCredentials_ReturnsToken()
        {
            // Arrange
            var userId = "testuser";
            var password = "Password123!";
            var hashedPassword = "hashedPassword";
            var user = new VendorUser { UserID = userId, Password = hashedPassword };

            _mockAuthRepository.Setup(repo => repo.Login(userId, password)).ReturnsAsync("fakeToken");

            // Act
            var result = await _authService.Login(userId, password);

            // Assert
            result.Should().Be("fakeToken");
        }
    }
}