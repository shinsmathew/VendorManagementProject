using Microsoft.EntityFrameworkCore;
using VendorManagementProject.DataBase;
using VendorManagementProject.Exceptions;
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

        public async Task<string> Register(VendorUser user)
        {
            if (await _context.VendorUsers.AnyAsync(u => u.UserID == user.UserID))
            {
                throw new UserAlreadyExistsException($"User with ID {user.UserID} already exists.");
            }
            
            user.Password = _passwordHasher.HashPassword(user.Password);
            _context.VendorUsers.Add(user);
            await _context.SaveChangesAsync();

            return _tokenService.GenerateToken(user);
        }

        public async Task<string> Login(string userId, string password)
        {
            var user = await _context.VendorUsers.FirstOrDefaultAsync(u => u.UserID == userId);

            if (user == null || !_passwordHasher.VerifyPassword(password, user.Password))
            {
                throw new InvalidCredentialsException("Invalid username or password.");
            }

            return _tokenService.GenerateToken(user);
        }
    }
}
