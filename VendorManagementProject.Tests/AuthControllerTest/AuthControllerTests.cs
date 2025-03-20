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
        public async Task Register_ValidUser_ReturnsOkWithToken()
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
            
            _mockAuthService.Setup(service => service.Register(user)).ReturnsAsync("fakeToken");

            // Act
            var result = await _authController.UserRegisteration(user);

            // Assert
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.Value.Should().BeEquivalentTo(new { Token = "fakeToken" });
        }

        [Fact]
        public async Task Login_ValidCredentials_ReturnsOkWithToken()
        {
            // Arrange
            _mockAuthService.Setup(service => service.Login("testuser", "Password@123")).ReturnsAsync("fakeToken");

            // Act
             var result = await _authController.UserLogin("testuser", "Password@123");

            // Assert
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.Value.Should().BeEquivalentTo(new { Token = "fakeToken" });
        }
    }
}