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

       
    }
}

