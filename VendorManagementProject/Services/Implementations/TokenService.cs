﻿using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VendorManagementProject.Models;
using VendorManagementProject.Services.Interface;

namespace VendorManagementProject.Services.Class
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public string GenerateToken(VendorUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);

            //Defines the properties of the JWT (claims, expiration, and signing credentials).
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.UID.ToString()),
                    new Claim(ClaimTypes.Name, user.UserID),
                    new Claim(ClaimTypes.Role, user.Role)
                }),

                Expires = DateTime.UtcNow.AddMinutes(30),
                Issuer = _configuration["Jwt:Issuer"], 
                Audience = _configuration["Jwt:Audience"], 
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
