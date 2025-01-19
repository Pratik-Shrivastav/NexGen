using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NextGen.Request;
using NextGen.Service;

namespace NexGen.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpPost("add")]
        public IActionResult AddAdmin([FromBody] AdminRequest admin)
        {
            _adminService.AddAdmin(admin);
            return Ok();
        }

        [Authorize]
        [HttpPut("update")]
        public IActionResult UpdateAdmin([FromBody] AdminRequest admin)
        {
            _adminService.UpdateAdmin(admin);
            return Ok();
        }

        [HttpPost("login")]
        public IActionResult LoginAdmin([FromBody] LoginRequest loginRequest)
        {
            _adminService.LoginAdmin(loginRequest);
            return Ok();
        }

        [HttpPost("verify-otp")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public IActionResult VerifyOtp([FromBody] VerifyOtpRequest verifyOtpRequest)
        {
            string token = _adminService.VerifyOtp(verifyOtpRequest);
            return Ok(token);
        }

        [HttpPost("{username}/logout")]
        public IActionResult Logout([FromRoute] string username)
        {
            _adminService.LogOut(username);
            return Ok();
        }

    }
}
