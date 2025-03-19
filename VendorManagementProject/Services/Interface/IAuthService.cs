using VendorManagementProject.Models;

namespace VendorManagementProject.Services.Interface
{
    public interface IAuthService
    {
        Task<string> Register(User user, string password);
        Task<string> Login(string userId, string password);
    }
}
