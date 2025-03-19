using Azure.Messaging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VendorManagementProject.Models;
using VendorManagementProject.Services.Interface;
using VendorManagementProject.Services.Interfaces;


namespace VendorManagementProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendorsController : ControllerBase
    {
        private readonly IVendorService _vendorService;
        private readonly IVendorRepository _vendorRepository;


        public VendorsController(IVendorService vendorService, IVendorRepository vendorRepository)
        {
            _vendorService = vendorService;
            _vendorRepository = vendorRepository;

        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vendor>>> GetVendors()
        {
                var vendor = await _vendorService.GetAllVendorsAsync();
                return Ok(vendor);

        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Vendor>> GetVendor(int id)
        {
            
                var vendor = await _vendorService.GetVendorByIdAsync(id);
                return Ok(vendor);
            
        }


        [Authorize(Policy = "AdminOnly")]
        [HttpPost]
        public async Task<ActionResult<Vendor>> CreateVendor(Vendor vendor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


                await _vendorService.AddVendorAsync(vendor);
                return CreatedAtAction(nameof(GetVendor), new { id = vendor.VendorID }, vendor);
            
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVendor(int id, Vendor vendor)
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            
                var existingVendor = await _vendorRepository.GetVendorByIdAsync(id);

                if (existingVendor == null)
                {
                    return NotFound($"Vendor with ID {id} not found.");
                }

                vendor.VendorID = existingVendor.VendorID;
                await _vendorService.UpdateVendorAsync(vendor);
                return Ok(vendor);
            
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVendor(int id)
        {

            await _vendorService.DeleteVendorAsync(id);
            return Ok("Vendor Deleted");

        }
    }
}
