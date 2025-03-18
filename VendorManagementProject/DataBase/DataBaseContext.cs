using Microsoft.EntityFrameworkCore;
using VendorManagementProject.Models;

namespace VendorManagementProject.DataBase
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions options) : base(options)
        {
        }


        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<ContactPerson> ContactPersons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vendor>()
                .HasMany(v => v.BankAccounts)
                .WithOne(b => b.Vendor)
                .HasForeignKey(b => b.VendorID);

            modelBuilder.Entity<Vendor>()
                .HasMany(v => v.ContactPersons)
                .WithOne(c => c.Vendor)
                .HasForeignKey(c => c.VendorID);
        }

    }

}
