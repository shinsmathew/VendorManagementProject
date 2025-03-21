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
using VendorManagementProject.Services.Interfaces;

namespace VendorManagementProject.Tests.AuthControllerTest
{
    public class VendorsControllerTests
    {
        private readonly Mock<IVendorService> _mockVendorService;
        private readonly VendorsController _vendorsController;

        public VendorsControllerTests()
        {
            _mockVendorService = new Mock<IVendorService>();
            _vendorsController = new VendorsController(_mockVendorService.Object);
        }

        [Fact]
        public async Task GetVendors_WhenVendorsExist_ReturnsOkWithVendors()
        {
            // Arrange
            var vendors = new List<Vendor>
            {
                new Vendor { VendorID = 1, VendorName = "Vendor1" },
                new Vendor { VendorID = 2, VendorName = "Vendor2" }
            };

            _mockVendorService.Setup(service => service.GetAllVendorsAsync()).ReturnsAsync(vendors);

            // Act
            var result = await _vendorsController.GetVendors();

            // Assert
            var okResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().BeEquivalentTo(vendors);
        }

        [Fact]
        public async Task GetVendorById_WithValidId_ReturnsOkWithVendor()
        {
            // Arrange
            var vendor = new Vendor { VendorID = 1, VendorName = "Vendor1" };
            _mockVendorService.Setup(service => service.GetVendorByIdAsync(1)).ReturnsAsync(vendor);

            // Act
            var result = await _vendorsController.GetVendor(1);

            // Assert
            var okResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().BeEquivalentTo(vendor);
        }

        [Fact]
        public async Task GetVendorById_WithInvalidId_ReturnsNotFound()
        {
            // Arrange
            _mockVendorService.Setup(service => service.GetVendorByIdAsync(999)).ThrowsAsync(new NotFoundException("Vendor not found."));

            // Act
            var result = await _vendorsController.GetVendor(999);

            // Assert
            var notFoundResult = result.Result.Should().BeOfType<NotFoundObjectResult>().Subject;
            notFoundResult.Value.Should().Be("Vendor not found.");
        }

        [Fact]
        public async Task CreateVendor_WithValidVendor_ReturnsCreatedAtAction()
        {
            // Arrange
            var vendor = new Vendor { VendorID = 1, VendorName = "Vendor1" };
            _mockVendorService.Setup(service => service.AddVendorAsync(vendor)).Returns(Task.CompletedTask);

            // Act
            var result = await _vendorsController.CreateVendor(vendor);

            // Assert
            var createdAtActionResult = result.Result.Should().BeOfType<CreatedAtActionResult>().Subject;
            createdAtActionResult.ActionName.Should().Be(nameof(VendorsController.GetVendor));
            createdAtActionResult.RouteValues["id"].Should().Be(1);
            createdAtActionResult.Value.Should().BeEquivalentTo(vendor);
        }

        [Fact]
        public async Task UpdateVendor_WithValidId_ReturnsOk()
        {
            // Arrange

             int Id = 1;
            var vendor = new Vendor 
            { 
                VendorID = 1,
                VendorName = "UpdatedVendor" 
            };


            _mockVendorService.Setup(service => service.UpdateVendorAsync(vendor)).Returns(Task.CompletedTask);

            // Act
            var result = await _vendorsController.UpdateVendor(Id, vendor);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().BeEquivalentTo(vendor);
        }

        [Fact]
        public async Task DeleteVendor_WithValidId_ReturnsOk()
        {
            // Arrange
            _mockVendorService.Setup(service => service.DeleteVendorAsync(1)).Returns(Task.CompletedTask);

            // Act
            var result = await _vendorsController.DeleteVendor(1);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().Be("Vendor Deleted");
        }
    }
}
