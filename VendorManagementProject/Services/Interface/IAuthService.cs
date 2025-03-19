using VendorManagementProject.Models;

namespace VendorManagementProject.Services.Interface
{
    public interface IAuthService
    {
        Task<string> Register(VendorUser user);
        Task<string> Login(string userId, string password);
    }
}
