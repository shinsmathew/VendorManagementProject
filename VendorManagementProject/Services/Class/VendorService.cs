using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Caching.Distributed;
using VendorManagementProject.Models;
using VendorManagementProject.Services.Interface;
using VendorManagementProject.Services.Interfaces;

namespace VendorManagementProject.Services.Class
{
    public class VendorService : IVendorService
    {
        private readonly IVendorRepository _vendorRepository;
        private readonly IRedisCacheService _redisCacheService;
        private readonly ILogger<VendorService> _logger;

        public VendorService(IVendorRepository vendorRepository, IRedisCacheService redisCacheService, ILogger<VendorService> logger)
        {
            _vendorRepository = vendorRepository;
            _redisCacheService = redisCacheService;
            _logger = logger;
        }

        public async Task<IEnumerable<Vendor>> GetAllVendorsAsync()
        {
            
                var cacheKey = "vendors_all";
                var cachedVendors = await _redisCacheService.GetFromCacheAsync<IEnumerable<Vendor>>(cacheKey);
                if (cachedVendors != null)
                {
                    return cachedVendors;
                }

                var vendors = await _vendorRepository.GetAllVendorsAsync();
                await _redisCacheService.SetCacheAsync(cacheKey, vendors, TimeSpan.FromHours(5));

                return vendors;
            
        }

        public async Task<Vendor> GetVendorByIdAsync(int id)
        {
           
                var cacheKey = $"vendor_{id}";
                var cachedVendor = await _redisCacheService.GetFromCacheAsync<Vendor>(cacheKey);
                if (cachedVendor != null)
                {
                    return cachedVendor;
                }

                var vendor = await _vendorRepository.GetVendorByIdAsync(id);
                if (vendor != null)
                {
                    await _redisCacheService.SetCacheAsync(cacheKey, vendor, TimeSpan.FromHours(5));
                }

                return vendor;
            
            
        }

        public async Task AddVendorAsync(Vendor vendor)
        {
            if (vendor == null)
            {
                throw new ArgumentNullException(nameof(vendor), "Vendor cannot be null.");
            }

                await _vendorRepository.AddVendorAsync(vendor);
                var cacheKey = $"vendor_{vendor.VendorID}";
                await _redisCacheService.SetCacheAsync(cacheKey, vendor, TimeSpan.FromHours(5));
                await _redisCacheService.RemoveFromCacheAsync("vendors_all");
            
        }

        public async Task UpdateVendorAsync(Vendor vendor)
        {

            if (vendor == null)
            {
                throw new ArgumentNullException(nameof(vendor), "Vendor cannot be null.");
            }

                await _vendorRepository.UpdateVendorAsync(vendor);
                var cacheKey = $"vendor_{vendor.VendorID}";
                await _redisCacheService.SetCacheAsync(cacheKey, vendor, TimeSpan.FromHours(5));
                await _redisCacheService.RemoveFromCacheAsync("vendors_all");
            

        }

        public async Task DeleteVendorAsync(int id)
        {

                await _vendorRepository.DeleteVendorAsync(id);
                await _redisCacheService.RemoveFromCacheAsync($"vendor_{id}");
                await _redisCacheService.RemoveFromCacheAsync("vendors_all");
          
        }

    }
}