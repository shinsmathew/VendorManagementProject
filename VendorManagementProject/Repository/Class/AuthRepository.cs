using Microsoft.EntityFrameworkCore;
using VendorManagementProject.DataBase;
using VendorManagementProject.Models;
using VendorManagementProject.Repository.Interfaces;
using VendorManagementProject.Services.Class;
using VendorManagementProject.Services.Interface;

namespace VendorManagementProject.Repository.Class
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataBaseContext _context;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenService _tokenService;
        public AuthRepository(DataBaseContext context, IPasswordHasher passwordHasher, ITokenService tokenService)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _tokenService = tokenService;
        }

        public async Task<string> Register(User user, string password)
        {
            if (await _context.Users.AnyAsync(u => u.UserID == user.UserID))
            {
                throw new Exception("User already exists.");
            }

            user.PasswordHash = _passwordHasher.HashPassword(password);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return _tokenService.GenerateToken(user);
        }

        public async Task<string> Login(string userId, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserID == userId);

            if (user == null || !_passwordHasher.VerifyPassword(password, user.PasswordHash))
            {
                throw new Exception("Invalid credentials.");
            }

            return _tokenService.GenerateToken(user);
        }
    }
}
