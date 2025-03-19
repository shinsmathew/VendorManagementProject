using VendorManagementProject.Models;

namespace VendorManagementProject.Repository.Interfaces
{
    public interface IAuthRepository
    {
        Task<string> Register(User user, string password);
        Task<string> Login(string userId, string password);
    }
}
