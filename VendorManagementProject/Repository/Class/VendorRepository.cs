using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Numerics;
using VendorManagementProject.DataBase;
using VendorManagementProject.Models;
using VendorManagementProject.Services.Interface;
using VendorManagementProject.Services.Interfaces;
using VendorManagementProject.Exceptions;

namespace VendorManagementProject.Services.Class
{
    public class VendorRepository : IVendorRepository
    {
        private readonly DataBaseContext _context;
        private readonly ILogger<VendorRepository> _logger;

        public VendorRepository(DataBaseContext context, ILogger<VendorRepository> logger)
        {
            _context = context;
            _logger= logger;
        }

        public async Task<IEnumerable<Vendor>> GetAllVendorsAsync()
        {
            var vendors = await _context.Vendors
                .Include(v => v.BankAccounts)
                .Include(v => v.ContactPersons)
                .ToListAsync();

            if (vendors == null || !vendors.Any())
            {
                throw new NotFoundException("No vendors found.");
            }

            return vendors;
        }

        public async Task<Vendor> GetVendorByIdAsync(int id)
        {
            

            var vendor = await _context.Vendors
               .Include(v => v.BankAccounts)
               .Include(v => v.ContactPersons)
               .FirstOrDefaultAsync(v => v.VendorID == id);

            if (vendor == null)
            {
                throw new NotFoundException($"Vendor with ID {id} not found.");
            }

            return vendor;
        }

        public async Task AddVendorAsync(Vendor vendor)
        {
            if (vendor == null)
            {
                throw new ArgumentNullException(nameof(vendor), "Vendor cannot be null.");
            }
            _context.Vendors.Add(vendor);
            await _context.SaveChangesAsync();
        }


        public async Task UpdateVendorAsync(Vendor vendor)
        {

            if (vendor == null)
            {
                throw new ArgumentNullException(nameof(vendor), "Vendor cannot be null.");
            }

            var existingVendor = await _context.Vendors
                .Include(v => v.BankAccounts)
                .Include(v => v.ContactPersons)
                .FirstOrDefaultAsync(v => v.VendorID == vendor.VendorID);

            if (existingVendor == null)
            {
                throw new NotFoundException($"Vendor with ID {vendor.VendorID} not found.");
            }

            _context.Entry(existingVendor).CurrentValues.SetValues(vendor);

            
            if (vendor.BankAccounts != null && vendor.BankAccounts.Any())
            {
               
                _context.BankAccounts.RemoveRange(existingVendor.BankAccounts);

                
                foreach (var bankAccount in vendor.BankAccounts)
                {
                    bankAccount.VendorID = existingVendor.VendorID;
                    _context.BankAccounts.Add(bankAccount);
                }
            }

            
            if (vendor.ContactPersons != null && vendor.ContactPersons.Any())
            {
                
                _context.ContactPersons.RemoveRange(existingVendor.ContactPersons);

                
                foreach (var contactPerson in vendor.ContactPersons)
                {
                    contactPerson.VendorID = existingVendor.VendorID;
                    _context.ContactPersons.Add(contactPerson);
                }
            }

          
            await _context.SaveChangesAsync();
        }


        public async Task DeleteVendorAsync(int id)
        {
            var vendor = await _context.Vendors.FindAsync(id);

            if (vendor == null)
            {
                throw new NotFoundException($"Vendor with ID {id} not found.");
            }

            if (vendor != null)
            {
                _context.Vendors.Remove(vendor);
                await _context.SaveChangesAsync();
            }
        }

        
    }
}