using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VendorManagementProject.Models;
using VendorManagementProject.Services.Interface;

namespace VendorManagementProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> UserRegisteration(User user, string password)
        {
            try
            {
                var token = await _authService.Register(user, password);
                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> UserLogin(string userId, string password)
        {
            try
            {
                var token = await _authService.Login(userId, password);
                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
