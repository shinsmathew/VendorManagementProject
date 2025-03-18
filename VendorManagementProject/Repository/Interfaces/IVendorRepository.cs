using VendorManagementProject.Models;

namespace VendorManagementProject.Services.Interfaces
{
    public interface IVendorRepository
    {
        Task<IEnumerable<Vendor>> GetAllVendorsAsync();
        Task<Vendor> GetVendorByIdAsync(int id);
        Task AddVendorAsync(Vendor vendor);
        Task UpdateVendorAsync(Vendor vendor);
        Task DeleteVendorAsync(int id);
    }
}
