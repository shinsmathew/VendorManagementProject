using VendorManagementProject.Models;

namespace VendorManagementProject.Services.Interface
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}
