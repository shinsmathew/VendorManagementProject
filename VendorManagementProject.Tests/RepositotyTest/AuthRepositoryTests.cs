using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendorManagementProject.DataBase;
using VendorManagementProject.Models;
using VendorManagementProject.Repository.Class;
using VendorManagementProject.Services.Interface;

namespace VendorManagementProject.Tests.RepositotyTest
{
    public class AuthRepositoryTests
    {
        private readonly Mock<DataBaseContext> _mockContext;
        private readonly Mock<IPasswordHasher> _mockPasswordHasher;
        private readonly Mock<ITokenService> _mockTokenService;
        private readonly AuthRepository _authRepository;

        public AuthRepositoryTests()
        {
            _mockContext = new Mock<DataBaseContext>();
            _mockPasswordHasher = new Mock<IPasswordHasher>();
            _mockTokenService = new Mock<ITokenService>();
            _authRepository = new AuthRepository(_mockContext.Object, _mockPasswordHasher.Object, _mockTokenService.Object);
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

            var mockDbSet = new Mock<DbSet<VendorUser>>();
            _mockContext.Setup(ctx => ctx.VendorUsers).Returns(mockDbSet.Object);
            _mockPasswordHasher.Setup(hasher => hasher.HashPassword(user.Password)).Returns("hashedPassword");
            _mockTokenService.Setup(service => service.GenerateToken(user)).Returns("fakeToken");

            // Act
            var result = await _authRepository.Register(user);

            // Assert
            result.Should().Be("fakeToken");
            mockDbSet.Verify(dbSet => dbSet.AddAsync(user, default), Times.Once);
            _mockContext.Verify(ctx => ctx.SaveChangesAsync(default), Times.Once);
        }

        [Fact]
        public async Task Login_WithValidCredentials_ReturnsToken()
        {
            // Arrange
            var user = new VendorUser { UserID = "john.doe", Password = "hashedPassword" };
            var mockDbSet = new Mock<DbSet<VendorUser>>();
            mockDbSet.Setup(dbSet => dbSet.FirstOrDefaultAsync(It.IsAny<System.Linq.Expressions.Expression<System.Func<VendorUser, bool>>>(), default))
                     .ReturnsAsync(user);
            _mockContext.Setup(ctx => ctx.VendorUsers).Returns(mockDbSet.Object);
            _mockPasswordHasher.Setup(hasher => hasher.VerifyPassword("Password@123", user.Password)).Returns(true);
            _mockTokenService.Setup(service => service.GenerateToken(user)).Returns("fakeToken");

            // Act
            var result = await _authRepository.Login("john.doe", "Password@123");

            // Assert
            result.Should().Be("fakeToken");
        }
    }
}