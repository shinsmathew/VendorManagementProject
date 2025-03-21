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

namespace VendorManagementProject.Tests.VendorControllerTest
{
    public class VendorsControllerTests
    {
        private readonly Mock<IVendorService> _mockVendorService;
        private readonly Mock<IVendorRepository> _mockVendorRepository;
        private readonly VendorsController _vendorsController;

        public VendorsControllerTests()
        {
            _mockVendorService = new Mock<IVendorService>();
            _mockVendorRepository = new Mock<IVendorRepository>();
            _vendorsController = new VendorsController(_mockVendorService.Object, _mockVendorRepository.Object);
        }

        // Test for GetVendors
        [Fact]
        public async Task GetVendors_ReturnsOkWithVendors()
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
            var okResult = result.Result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.Value.Should().BeEquivalentTo(vendors);
        }

        

        // Test for GetVendorById
        [Fact]
        public async Task GetVendorById_ValidId_ReturnsOkWithVendor()
        {
            // Arrange
            var vendor = new Vendor { VendorID = 1, VendorName = "Vendor1" };
            _mockVendorService.Setup(service => service.GetVendorByIdAsync(1)).ReturnsAsync(vendor);

            // Act
            var result = await _vendorsController.GetVendor(1);

            // Assert
            var okResult = result.Result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.Value.Should().BeEquivalentTo(vendor);
        }


        // Test for CreateVendor
        [Fact]
        public async Task CreateVendor_ValidVendor_ReturnsCreatedAtAction()
        {
            // Arrange
            var vendor = new Vendor { VendorID = 1, VendorName = "Vendor1" };
            _mockVendorService.Setup(service => service.AddVendorAsync(vendor)).Returns(Task.CompletedTask);

            // Act
            var result = await _vendorsController.CreateVendor(vendor);

            // Assert
            var createdAtActionResult = result.Result as CreatedAtActionResult;
            createdAtActionResult.Should().NotBeNull();
            createdAtActionResult.ActionName.Should().Be(nameof(VendorsController.GetVendor));
            createdAtActionResult.RouteValues["id"].Should().Be(1);
            createdAtActionResult.Value.Should().BeEquivalentTo(vendor);
        }

        [Fact]
        public async Task CreateVendor_InvalidModelState_ReturnsBadRequest()
        {
            // Arrange
            var vendor = new Vendor { VendorID = 1, VendorName = "" }; // Invalid model
            _vendorsController.ModelState.AddModelError("VendorName", "VendorName is required.");

            // Act
            var result = await _vendorsController.CreateVendor(vendor);

            // Assert
            var badRequestResult = result.Result as BadRequestObjectResult;
            badRequestResult.Should().NotBeNull();
            badRequestResult.Value.Should().BeOfType<SerializableError>();
        }

        

        [Fact]
        public async Task UpdateVendor_InvalidId_ReturnsNotFound()
        {
            // Arrange
            var vendor = new Vendor { VendorID = 1, VendorName = "UpdatedVendor" };
            _mockVendorService.Setup(service => service.UpdateVendorAsync(vendor)).ThrowsAsync(new NotFoundException("Vendor with ID 1 not found."));

            // Act
            var result = await _vendorsController.UpdateVendor(1, vendor);

            // Assert
            var notFoundResult = result as NotFoundObjectResult;
            notFoundResult.Should().NotBeNull();
            notFoundResult.Value.Should().Be("Vendor with ID 1 not found.");
        }

        [Fact]
        public async Task UpdateVendor_InvalidModelState_ReturnsBadRequest()
        {
            // Arrange
            var vendor = new Vendor { VendorID = 1, VendorName = "" }; // Invalid model
            _vendorsController.ModelState.AddModelError("VendorName", "VendorName is required.");

            // Act
            var result = await _vendorsController.UpdateVendor(1, vendor);

            // Assert
            var badRequestResult = result as BadRequestObjectResult;
            badRequestResult.Should().NotBeNull();
            badRequestResult.Value.Should().BeOfType<SerializableError>();
        }

        // Test for DeleteVendor
        [Fact]
        public async Task DeleteVendor_ValidId_ReturnsOk()
        {
            // Arrange
            _mockVendorService.Setup(service => service.DeleteVendorAsync(1)).Returns(Task.CompletedTask);

            // Act
            var result = await _vendorsController.DeleteVendor(1);

            // Assert
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.Value.Should().Be("Vendor Deleted");
        }

       
    }
}

