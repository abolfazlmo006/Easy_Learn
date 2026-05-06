using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Easy_learn.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthenticationController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<Response>> Register(RegisterDto dto)
        {
            var result = await _userService.Register(dto);
            return Ok(result);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<LoginResponse>> Login(LoginDto dto)
        {
            var result = await _userService.Login(dto);
            return Ok(result);
        }

        [HttpPut("ConfirmEmail")]
        public async Task<ActionResult> ConfirmEmail([FromBody] ConfirmEmailDto dto)
        {
            var result = await _userService.ConfirmEmail(dto.UserName, dto.Token);
            if (!result.SuccessFul)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}
