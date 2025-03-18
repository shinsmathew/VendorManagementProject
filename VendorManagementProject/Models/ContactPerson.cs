using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace VendorManagementProject.Models
{
    public class ContactPerson
    {
        [Key]
        [JsonIgnore]
        public int ContactPID { get; set; }

        [Required(ErrorMessage = "FirstName is required.")]
        [StringLength(50, ErrorMessage = "FirstName cannot exceed 50 characters.")]
        public string FirstName { get; set; }

       
        [Required(ErrorMessage = "LastName is required.")]
        [StringLength(50, ErrorMessage = "LastName cannot exceed 50 characters.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Mobile is required.")]
        [Phone(ErrorMessage = "Invalid mobile number.")]
        public string Mobile { get; set; }

        [Required(ErrorMessage = "EMail is required.")]
        [EmailAddress(ErrorMessage = "Invalid EMail Address.")]
        public string EMail { get; set; }

        [JsonIgnore]
        public int VendorID { get; set; }
        [JsonIgnore]
        [ValidateNever]
        public Vendor Vendor { get; set; }
    }
}

