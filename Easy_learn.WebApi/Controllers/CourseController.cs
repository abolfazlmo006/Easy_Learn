global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Easy_learn.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseSerivce;
        private readonly ICommentService _commentService;
        public CourseController(ICourseService courseSerivce, ICommentService commentService)
        {
            _courseSerivce = courseSerivce;
            _commentService = commentService;
        }

        // GET: api/<CourseController>
        [HttpGet]
        public async Task<ActionResult<List<CourseListDto>>> Get()
        {
            var courses = await _courseSerivce.GetAll();
            return Ok(courses);
        }

        // GET api/<CourseController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CourseDetailDto>> Get(int id)
        {
            var course = await _courseSerivce.Get(id , User.Identity.Name);

            course.Comments = await _commentService.GetByCourse(course.Id,User.Identity.Name);
           
            return Ok(course);
        }

        // POST api/<CourseController>
        [HttpPost]
        [Authorize(Roles = "Teacher")]
        public async Task<ActionResult<Response>> Post([FromBody] CreateCourseDto dto)
        {
            var result = await _courseSerivce.Add(dto, User.Identity.Name);
            return Ok(result);
        }

        // PUT api/<CourseController>/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Teacher")]
        public async Task<ActionResult<Response>> Put(int id, [FromBody] UpdateCourseDto dto)
        {
            var result = await _courseSerivce.Update(dto, id);
            return Ok(result);
        }

        // DELETE api/<CourseController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int id)
        {
            await _courseSerivce.Delete(id);
            return NoContent();
        }

        [HttpGet("GetByAdmin")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<CourseDetailDto>>> GetByAdmin()
        {
            var courses = await _courseSerivce.GetByAdmin();

            return Ok(courses);
        }

        [HttpPost("Verify/{CourseId}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Verify(int CourseId)
        {
            await _courseSerivce.Verify(CourseId);

            return NoContent();
        }

        [HttpGet("GetByUser")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<List<CourseDetailDto>>> GetByUser()
        {
            var result = await _courseSerivce.GetByUser(User.Identity.Name);
            return Ok(result);
        }

        [HttpGet("GetByTeacher")]
        [Authorize(Roles = "Teacher")]
        public async Task<ActionResult<List<CourseListDto>>> GetByTeacher()
        {
            var result = await _courseSerivce.GetByTeacher(User.Identity.Name);
            return Ok(result);
        }

        [HttpGet("GetByCategory/{categoryId}")]
        public async Task<ActionResult<List<CourseListDto>>> GetByCategory(int categoryId)
        {
            var result = await _courseSerivce.GetByCategory(categoryId);
            return Ok(result);
        }
    }
}
