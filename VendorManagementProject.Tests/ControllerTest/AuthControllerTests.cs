using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendorManagementProject.Controllers;
using VendorManagementProject.Exceptions;
using VendorManagementProject.Models;
using VendorManagementProject.Services.Interface;


namespace VendorManagementProject.Tests.AuthControllerTest
{
    public class AuthControllerTests
    {
        private readonly Mock<IAuthService> _mockAuthService;
        private readonly AuthController _authController;

        public AuthControllerTests()
        {
            _mockAuthService = new Mock<IAuthService>();
            _authController = new AuthController(_mockAuthService.Object);
        }

        [Fact]
        public async Task Register_WithValidUser_ReturnsOkWithToken()
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

            _mockAuthService.Setup(service => service.Register(user)).ReturnsAsync("Generated_Fake_Token");

            // Act
            var result = await _authController.UserRegistration(user);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().BeEquivalentTo(new { Token = "Generated_Fake_Token" });
            _mockAuthService.Verify(service => service.Register(user), Times.Once);
        }

        [Fact]
        public async Task Register_WithExistingUser_ReturnsBadRequest()
        {
            // Arrange
            var user = new VendorUser
            {
                UserID = "Shins.Mathew",
                Password = "Shins@123"
            };

            _mockAuthService.Setup(service => service.Register(user)).ThrowsAsync(new Exception("User already exists."));

            // Act
            var result = await _authController.UserRegistration(user);

            // Assert
            var badRequestResult = result.Should().BeOfType<BadRequestObjectResult>().Subject;
            badRequestResult.Value.Should().Be("User already exists.");
        }

        [Fact]
        public async Task Login_WithValidCredentials_ReturnsOkWithToken()
        {
            // Arrange
            _mockAuthService.Setup(service => service.Login("Shins.Mathew", "Shins@123")).ReturnsAsync("Generated_Fake_Token");

            // Act
            var result = await _authController.UserLogin("Shins.Mathew", "Shins@123");

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().BeEquivalentTo(new { Token = "Generated_Fake_Token" });
            _mockAuthService.Verify(service => service.Login("Shins.Mathew", "Shins@123"), Times.Once);
        }

        [Fact]
        public async Task Login_WithInvalidCredentials_ReturnsBadRequest()
        {
            // Arrange
            _mockAuthService.Setup(service => service.Login("Shinhew", "psword")).ThrowsAsync(new Exception("Invalid credentials."));

            // Act
            var result = await _authController.UserLogin("Shinhew", "psword");

            // Assert
            var badRequestResult = result.Should().BeOfType<BadRequestObjectResult>().Subject;
            badRequestResult.Value.Should().Be("Invalid credentials.");
        }
    }
}