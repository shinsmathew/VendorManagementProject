using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace VendorManagementProject.Models
{
    public class BankAccount
    {
        [Key]
        [JsonIgnore]
        public int BankID { get; set; }

        [Required(ErrorMessage = "IBAN is required.")]
        [RegularExpression(@"^[A-Z]{2}\d{20}$", ErrorMessage = "IBAN must start with 2 capital letters followed by 20 digits.")]
        public string IBAN { get; set; }

        [Required(ErrorMessage = "BIC is required.")]
        [RegularExpression(@"^[A-Z]{6}[A-Z0-9]{2}([A-Z0-9]{3})?$", ErrorMessage = "BIC must be 8 or 11 alphanumeric characters, all uppercase.")]
        public string BIC { get; set; }

        [Required(ErrorMessage = "BankName is required.")]
        [StringLength(100, ErrorMessage = "BankName cannot exceed 100 characters.")]
        public string BankName { get; set; }

        [Required(ErrorMessage = "AccountHolder is required.")]
        [StringLength(50, ErrorMessage = "AccountHolder cannot exceed 50 characters.")]
        public string AccountHolder { get; set; }

        [JsonIgnore]
        public int VendorID { get; set; }
        [JsonIgnore]
        [ValidateNever]
        public Vendor Vendor { get; set; }
    }
}