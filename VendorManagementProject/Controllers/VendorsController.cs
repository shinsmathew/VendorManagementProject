using Azure.Messaging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VendorManagementProject.Middleware;
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vendor>>> GetVendors()
        {
            var vendor = await _vendorService.GetAllVendorsAsync();
            if (vendor == null)
            {
                return NotFound("Empty");
            }
            return Ok(vendor);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Vendor>> GetVendor(int id)
        {
            var vendor = await _vendorService.GetVendorByIdAsync(id);
            if (vendor == null)
            {
                return NotFound("Vendor not found.");
            }
            return Ok(vendor);
        }

        [HttpPost]
        public async Task<ActionResult<Vendor>> CreateVendor(Vendor vendor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {

                await _vendorService.AddVendorAsync(vendor);
                return CreatedAtAction(nameof(GetVendor), new { id = vendor.VendorID }, vendor);
            }
            catch (ExceptionMiddleware.ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVendor(int id, Vendor vendor)
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {


                var existingVendor = await _vendorRepository.GetVendorByIdAsync(id);

                if (existingVendor == null)
                {
                    return NotFound("Vendor not found.");
                }

                vendor.VendorID = existingVendor.VendorID;
                await _vendorService.UpdateVendorAsync(vendor);
                return Ok(vendor);
            }
            catch (ExceptionMiddleware.NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVendor(int id)
        {
            try
            {
                await _vendorService.DeleteVendorAsync(id);
                return Ok();
            }
            catch (ExceptionMiddleware.NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }

        }
    }
}
