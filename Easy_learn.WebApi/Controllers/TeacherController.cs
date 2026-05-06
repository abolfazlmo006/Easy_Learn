using Easy_learn.WebApi.DTOs.TeacherDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Easy_learn.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService _service;
        private readonly UserManager<UserEntity> _userManager;
        public TeacherController(ITeacherService service, UserManager<UserEntity> userManager)
        {
            _service = service;
            _userManager = userManager;
        }

        [HttpPost("RequestForTeacher")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult> RequestForTeacher(RequestForTeacherDto dto)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var Roles = await _userManager.GetRolesAsync(user);
            if (Roles.Count == 1)
            {
                var result = await _service.RequestForTeacher(dto, User.Identity.Name);
                return Ok(result);
            }
            return Forbid();
        }

        [HttpPost("VerifyRequestForTeacher/{RequestForTeacher_Id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> VerifyRequestForTeacher(int RequestForTeacher_Id)
        {
            await _service.VerifyRequestForTeacher(RequestForTeacher_Id);

            return NoContent();
        }

        [HttpGet("GetRequestForTeacher")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<GetRequestForTeacherDto>>> GetRequestForTeacher()
        {
            var result = await _service.GetRequestForTeacher();
            return Ok(result);
        }
    }
}
