using VendorManagementProject.Models;

namespace VendorManagementProject.Repository.Interfaces
{
    public interface IAuthRepository
    {
        Task<string> Register(VendorUser user);
        Task<string> Login(string userId, string password);
    }
}
