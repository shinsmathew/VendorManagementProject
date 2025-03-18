using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace VendorManagementProject.Models
{
    public class Vendor
    {
        [Key]
        [JsonIgnore]
        public int VendorID { get; set; }


        [Required(ErrorMessage = "VendorName is required.")]
        [StringLength(100, ErrorMessage = "VendorName cannot exceed 100 characters.")]
        public string VendorName { get; set; }

        public string VendorName2 { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        [StringLength(50, ErrorMessage = "VendorName cannot exceed 50 characters.")]
        public string Address1 { get; set; }

        public string Address2 { get; set; }

        [Required(ErrorMessage = "ZIP is required.")]
        [RegularExpression(@"^\d{5}(-\d{4})?$", ErrorMessage = "ZIP must be in the format 12345 or 12345-6789.")]
        public string ZIP { get; set; }

        [Required(ErrorMessage = "Country is required.")]
        [StringLength(50, ErrorMessage = "Country cannot exceed 50 characters.")]
        public string Country { get; set; }

        [Required(ErrorMessage = "City is required.")]
        [StringLength(50, ErrorMessage = "City cannot exceed 50 characters.")]
        public string City { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string EMail { get; set; }

        public string Phone { get; set; }

        [Required(ErrorMessage = "Mobile is required.")]
        [Phone(ErrorMessage = "Invalid mobile number.")]
        public string Mobile { get; set; }

        public string Notes { get; set; }

        // Navigation properties
        public ICollection<BankAccount> BankAccounts { get; set; }
        public ICollection<ContactPerson> ContactPersons { get; set; }


    }
}
