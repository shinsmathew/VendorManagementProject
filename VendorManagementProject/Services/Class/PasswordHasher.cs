using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using VendorManagementProject.Services.Interface;

namespace VendorManagementProject.Services.Class
{
    public class PasswordHasher : IPasswordHasher
    {
        public string HashPassword(string password)
        {
            if (!IsPasswordComplex(password))
            {
                throw new ValidationException("Password does not meet complexity requirements.");
            }
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }

        private bool IsPasswordComplex(string password)
        {
            // Regex to enforce complexity: at least 8 characters, one uppercase, one lowercase, one number, and one special character
            var regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$");
            return regex.IsMatch(password);
        }
    }
}
