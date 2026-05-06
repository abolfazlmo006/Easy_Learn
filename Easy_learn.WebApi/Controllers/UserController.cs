using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Easy_learn.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet("ForgotPassword/{Email}")]
        public async Task<ActionResult<Response>> ForgotPassword(string Email)
        {
            var result = await _userService.ForgotPassword(Email);
            return Ok(result);
        }
        [HttpPost("ResetPassword")]
        public async Task<ActionResult<Response>> ResetPassword([FromBody] ResetPasswordDto dto)
        {
            var result = await _userService.ResetPassword(dto);
            return Ok(result);
        }

        [Authorize(Roles = "User")]
        [HttpPut("ChangePassword")]
        public async Task<ActionResult<Response>> ChangePassword(ChangePasswordDto dto)
        {
            var result = await _userService.ChangePassword(dto, User.Identity.Name);
            return Ok(result);
        }
        [Authorize(Roles = "User")]
        [HttpGet("Profile")]
        public async Task<ActionResult<ProfileUserDto>> Profile()
        {
            var result = await _userService.GetProfile(User.Identity.Name);
            return Ok(result);
        }
    }
}
