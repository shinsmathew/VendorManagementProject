using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendorManagementProject.Models;
using VendorManagementProject.Services.Class;
using VendorManagementProject.Services.Interface;
using VendorManagementProject.Services.Interfaces;

namespace VendorManagementProject.Tests.ServicesTest
{
    public class VendorServiceTests
    {
        private readonly Mock<IVendorRepository> _mockVendorRepository;
        private readonly Mock<IRedisCacheService> _mockRedisCacheService;
        private readonly VendorService _vendorService;

        public VendorServiceTests()
        {
            _mockVendorRepository = new Mock<IVendorRepository>();
            _mockRedisCacheService = new Mock<IRedisCacheService>();
            _vendorService = new VendorService(_mockVendorRepository.Object, _mockRedisCacheService.Object, null);
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

            _mockVendorRepository.Setup(repo => repo.GetAllVendorsAsync()).ReturnsAsync(vendors);

            // Act
            var result = await _vendorService.GetAllVendorsAsync();

            // Assert
            result.Should().BeEquivalentTo(vendors);
            _mockRedisCacheService.Verify(cache => cache.SetCacheAsync("vendors_all", vendors, It.IsAny<TimeSpan>()), Times.Once);
        }

        [Fact]
        public async Task GetVendorByIdAsync_WithValidId_ReturnsVendor()
        {
            // Arrange
            var vendor = new Vendor { VendorID = 1, VendorName = "Vendor1" };
            _mockVendorRepository.Setup(repo => repo.GetVendorByIdAsync(1)).ReturnsAsync(vendor);

            // Act
            var result = await _vendorService.GetVendorByIdAsync(1);

            // Assert
            result.Should().BeEquivalentTo(vendor);
            _mockRedisCacheService.Verify(cache => cache.SetCacheAsync("vendor_1", vendor, It.IsAny<TimeSpan>()), Times.Once);
        }

        [Fact]
        public async Task AddVendorAsync_WithValidVendor_AddsVendor()
        {
            // Arrange
            var vendor = new Vendor { VendorID = 1, VendorName = "Vendor1" };
            _mockVendorRepository.Setup(repo => repo.AddVendorAsync(vendor)).Returns(Task.CompletedTask);

            // Act
            await _vendorService.AddVendorAsync(vendor);

            // Assert
            _mockVendorRepository.Verify(repo => repo.AddVendorAsync(vendor), Times.Once);
            _mockRedisCacheService.Verify(cache => cache.RemoveFromCacheAsync("vendors_all"), Times.Once);
        }

        [Fact]
        public async Task UpdateVendorAsync_WithValidVendor_UpdatesVendor()
        {
            // Arrange
            var vendor = new Vendor { VendorID = 1, VendorName = "UpdatedVendor" };
            _mockVendorRepository.Setup(repo => repo.UpdateVendorAsync(vendor)).Returns(Task.CompletedTask);

            // Act
            await _vendorService.UpdateVendorAsync(vendor);

            // Assert
            _mockVendorRepository.Verify(repo => repo.UpdateVendorAsync(vendor), Times.Once);
            _mockRedisCacheService.Verify(cache => cache.RemoveFromCacheAsync("vendors_all"), Times.Once);
        }

        [Fact]
        public async Task DeleteVendorAsync_WithValidId_DeletesVendor()
        {
            // Arrange
            _mockVendorRepository.Setup(repo => repo.DeleteVendorAsync(1)).Returns(Task.CompletedTask);

            // Act
            await _vendorService.DeleteVendorAsync(1);

            // Assert
            _mockVendorRepository.Verify(repo => repo.DeleteVendorAsync(1), Times.Once);
            _mockRedisCacheService.Verify(cache => cache.RemoveFromCacheAsync("vendor_1"), Times.Once);
            _mockRedisCacheService.Verify(cache => cache.RemoveFromCacheAsync("vendors_all"), Times.Once);
        }
    }
}