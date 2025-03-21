using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendorManagementProject.DataBase;
using VendorManagementProject.Exceptions;
using VendorManagementProject.Models;
using VendorManagementProject.Services.Class;

namespace VendorManagementProject.Tests.RepositotyTest
{
    public class VendorRepositoryTests
    {
        private readonly Mock<DataBaseContext> _mockContext;
        private readonly VendorRepository _vendorRepository;

        public VendorRepositoryTests()
        {
            _mockContext = new Mock<DataBaseContext>();
            _vendorRepository = new VendorRepository(_mockContext.Object, null);
        }

        [Fact]
        public async Task GetAllVendorsAsync_WhenVendorsExist_ReturnsVendors()
        {
            // Arrange
            var vendors = new List<Vendor>
            {
                new Vendor { VendorID = 1, VendorName = "Vendor1" },
                new Vendor { VendorID = 2, VendorName = "Vendor2" }
            };

            var mockDbSet = new Mock<DbSet<Vendor>>();
            mockDbSet.As<IQueryable<Vendor>>().Setup(m => m.Provider).Returns(vendors.AsQueryable().Provider);
            mockDbSet.As<IQueryable<Vendor>>().Setup(m => m.Expression).Returns(vendors.AsQueryable().Expression);
            mockDbSet.As<IQueryable<Vendor>>().Setup(m => m.ElementType).Returns(vendors.AsQueryable().ElementType);
            mockDbSet.As<IQueryable<Vendor>>().Setup(m => m.GetEnumerator()).Returns(vendors.AsQueryable().GetEnumerator());

            _mockContext.Setup(ctx => ctx.Vendors).Returns(mockDbSet.Object);

            // Act
            var result = await _vendorRepository.GetAllVendorsAsync();

            // Assert
            result.Should().BeEquivalentTo(vendors);
        }

        [Fact]
        public async Task GetVendorByIdAsync_WithValidId_ReturnsVendor()
        {
            // Arrange
            var vendor = new Vendor { VendorID = 1, VendorName = "Vendor1" };
            var mockDbSet = new Mock<DbSet<Vendor>>();
            mockDbSet.Setup(dbSet => dbSet.FindAsync(1)).ReturnsAsync(vendor);
            _mockContext.Setup(ctx => ctx.Vendors).Returns(mockDbSet.Object);

            // Act
            var result = await _vendorRepository.GetVendorByIdAsync(1);

            // Assert
            result.Should().BeEquivalentTo(vendor);
        }

        [Fact]
        public async Task GetVendorByIdAsync_WithInvalidId_ThrowsNotFoundException()
        {
            // Arrange
            var mockDbSet = new Mock<DbSet<Vendor>>();
            mockDbSet.Setup(dbSet => dbSet.FindAsync(999)).ReturnsAsync((Vendor)null);
            _mockContext.Setup(ctx => ctx.Vendors).Returns(mockDbSet.Object);

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => _vendorRepository.GetVendorByIdAsync(999));
        }

        [Fact]
        public async Task AddVendorAsync_WithValidVendor_AddsVendor()
        {
            // Arrange
            var vendor = new Vendor { VendorID = 1, VendorName = "Vendor1" };
            var mockDbSet = new Mock<DbSet<Vendor>>();
            _mockContext.Setup(ctx => ctx.Vendors).Returns(mockDbSet.Object);

            // Act
            await _vendorRepository.AddVendorAsync(vendor);

            // Assert
            mockDbSet.Verify(dbSet => dbSet.AddAsync(vendor, default), Times.Once);
            _mockContext.Verify(ctx => ctx.SaveChangesAsync(default), Times.Once);
        }

        [Fact]
        public async Task UpdateVendorAsync_WithValidVendor_UpdatesVendor()
        {
            // Arrange
            var vendor = new Vendor { VendorID = 1, VendorName = "UpdatedVendor" };
            var mockDbSet = new Mock<DbSet<Vendor>>();
            _mockContext.Setup(ctx => ctx.Vendors).Returns(mockDbSet.Object);

            // Act
            await _vendorRepository.UpdateVendorAsync(vendor);

            // Assert
            _mockContext.Verify(ctx => ctx.SaveChangesAsync(default), Times.Once);
        }

        [Fact]
        public async Task DeleteVendorAsync_WithValidId_DeletesVendor()
        {
            // Arrange
            var vendor = new Vendor { VendorID = 1, VendorName = "Vendor1" };
            var mockDbSet = new Mock<DbSet<Vendor>>();
            mockDbSet.Setup(dbSet => dbSet.FindAsync(1)).ReturnsAsync(vendor);
            _mockContext.Setup(ctx => ctx.Vendors).Returns(mockDbSet.Object);

            // Act
            await _vendorRepository.DeleteVendorAsync(1);

            // Assert
            mockDbSet.Verify(dbSet => dbSet.Remove(vendor), Times.Once);
            _mockContext.Verify(ctx => ctx.SaveChangesAsync(default), Times.Once);
        }
    }
}