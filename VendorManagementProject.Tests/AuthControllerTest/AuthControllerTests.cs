using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendorManagementProject.Controllers;
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
        public async Task Register_ShouldReturnOk_WhenUserIsValid()
        {
            // Arrange
            var user = new VendorUser { UserID = "shinsmw@gmail.com", Password = "ShinsM" };
            _mockAuthService.Setup(service => service.Register(user)).ReturnsAsync("generated-token");

            // Act
            var result = await _authController.UserRegisteration(user);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("generated-token", ((dynamic)okResult.Value).Token);
        }

        [Fact]
        public async Task Register_ShouldReturnBadRequest_WhenExceptionIsThrown()
        {
            // Arrange
            var user = new VendorUser { UserID = "shinsmathew@gmail.com", Password = "ShinsM" };
            _mockAuthService.Setup(service => service.Register(user)).ThrowsAsync(new Exception("User already exists."));

            // Act
            var result = await _authController.UserRegisteration(user);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("User already exists.", badRequestResult.Value);
        }

        [Fact]
        public async Task Login_ShouldReturnOk_WhenCredentialsAreValid()
        {
            // Arrange
            var userId = "shinsmathew@gmail.com";
            var password = "ShinsM";
            _mockAuthService.Setup(service => service.Login(userId, password)).ReturnsAsync("generated-token");

            // Act
            var result = await _authController.UserLogin(userId, password);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("generated-token", ((dynamic)okResult.Value).Token);
        }

        [Fact]
        public async Task Login_ShouldReturnBadRequest_WhenCredentialsAreInvalid()
        {
            // Arrange
            var userId = "shins3456mathew@gmail.com";
            var password = "ShinsM";
            _mockAuthService.Setup(service => service.Login(userId, password)).ThrowsAsync(new Exception("Invalid credentials."));

            // Act
            var result = await _authController.UserLogin(userId, password);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Invalid credentials.", badRequestResult.Value);
        }
    }
}