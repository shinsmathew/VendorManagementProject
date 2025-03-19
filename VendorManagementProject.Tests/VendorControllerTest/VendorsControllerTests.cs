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

namespace VendorManagementProject.Tests.VendorControllerTest
{
    public class VendorsControllerTests
    {
        private readonly Mock<IVendorService> _mockVendorService;
        private readonly VendorsController _vendorsController;

        public VendorsControllerTests()
        {
            _mockVendorService = new Mock<IVendorService>();
            _vendorsController = new VendorsController(_mockVendorService.Object, null);
        }

       
        [Fact]
        public async Task GetVendors_ShouldReturnOk_WhenVendorsExist()
        {
            // Arrange
            var vendors = new List<Vendor> { new Vendor { VendorID = 1, VendorName = "Test Vendor" } };
            _mockVendorService.Setup(service => service.GetAllVendorsAsync()).ReturnsAsync(vendors);

            // Act
            var result = await _vendorsController.GetVendors();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(vendors, okResult.Value);
        }

        
        [Fact]
        public async Task GetVendor_ShouldReturnOk_WhenVendorExists()
        {
            // Arrange
            var vendor = new Vendor { VendorID = 1, VendorName = "Test Vendor" };
            _mockVendorService.Setup(service => service.GetVendorByIdAsync(1)).ReturnsAsync(vendor);

            // Act
            var result = await _vendorsController.GetVendor(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(vendor, okResult.Value);
        }

        [Fact]
        public async Task GetVendor_ShouldReturnNotFound_WhenVendorDoesNotExist()
        {
            // Arrange
            _mockVendorService.Setup(service => service.GetVendorByIdAsync(1)).ThrowsAsync(new Exception("Vendor not found."));

            // Act
            var result = await _vendorsController.GetVendor(1);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Vendor not found.", notFoundResult.Value);
        }

        [Fact]
        public async Task DeleteVendor_ShouldReturnOk_WhenVendorIsDeleted()
        {
            // Arrange
            _mockVendorService.Setup(service => service.DeleteVendorAsync(1)).Returns(Task.CompletedTask);

            // Act
            var result = await _vendorsController.DeleteVendor(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Vendor Deleted", okResult.Value);
        }


        [Fact]
        public async Task DeleteVendor_ShouldReturnNotFound_WhenVendorDoesNotExist()
        {
            // Arrange
            _mockVendorService.Setup(service => service.DeleteVendorAsync(1)).ThrowsAsync(new Exception("Vendor not found."));

            // Act
            var result = await _vendorsController.DeleteVendor(1);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Vendor not found.", notFoundResult.Value);
        }
    }
}

