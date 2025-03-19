using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace VendorManagementProject.Models
{
    public class VendorUser
    {
        [Key]
        [JsonIgnore]
        public int UID { get; set; }

        
        [Required(ErrorMessage = "UserFirstName is required.")]
        [StringLength(50, ErrorMessage = "UserFirstName cannot exceed 50 characters.")]
        public string UserFirstName { get; set; }

        [Required(ErrorMessage = "UserLastName is required.")]
        [StringLength(50, ErrorMessage = "UserLastName cannot exceed 50 characters.")]
        public string UserLastName { get; set; }

        [Required(ErrorMessage = "UserID is required.")]
        [StringLength(50, ErrorMessage = "UserID cannot exceed 50 characters.")]
        public string UserID { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Role is required.")]
        [StringLength(20, ErrorMessage = "Role cannot exceed 20 characters.")]
        public string Role { get; set; }

        [JsonIgnore]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}
