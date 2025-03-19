using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using VendorManagementProject.DataBase;
using VendorManagementProject.Models;
using VendorManagementProject.Repository.Class;
using VendorManagementProject.Repository.Interfaces;
using VendorManagementProject.Services.Interface;
using VendorManagementProject.Services.Interfaces;

namespace VendorManagementProject.Services.Class
{
    public class AuthService : IAuthService
    {

        private readonly IAuthRepository _authRepository;

        public AuthService(IAuthRepository authRepository)
        {

            _authRepository= authRepository;
        }

        public async Task<string> Register(VendorUser user)
        {

            var AuthToken = await _authRepository.Register(user);
            return AuthToken;
        }

        public async Task<string> Login(string userId, string password)
        {

            var AuthToken = await _authRepository.Login( userId, password);
            return AuthToken;


        }
    }
}
