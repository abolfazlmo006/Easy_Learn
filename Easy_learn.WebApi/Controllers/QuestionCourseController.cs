using Easy_learn.WebApi.DTOs.QuestionCourseDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Easy_learn.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionCourseController : ControllerBase
    {
        private readonly IQuestionCourseService _questionCourseService;

        public QuestionCourseController(IQuestionCourseService questionCourseService)
        {
            _questionCourseService = questionCourseService;
        }

        [HttpGet("GetForCourse/{CourseId}")]
        public async Task<ActionResult<List<GetQuestionCourseListDto>>> GetForCourse(int CourseId)
        {
            var questions = await _questionCourseService.GetQuestionsForCourse(CourseId);
            return Ok(questions);
        }

        [Authorize]
        [HttpGet("GetForUser")]
        public async Task<ActionResult<List<GetQuestionCourseListDto>>> GetForUser()
        {
            var questions = await _questionCourseService.GetQuestionsForUser(User.Identity.Name);
            return Ok(questions);
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<GetQuestionCourseDetailDto>> Get(int Id)
        {
            var question = await _questionCourseService.GetQuestionDetail(Id);
            return Ok(question);
        }

        [HttpDelete("{Id}")]
        [Authorize]
        public async Task<ActionResult> Delete(int Id)
        {
            await _questionCourseService.Delete(Id, User.Identity.Name);
            return NoContent();
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Response>> Post([FromBody] CreateQuestionCourseDto dto)
        {
            var result = await _questionCourseService.Add(dto, User.Identity.Name);
            return Ok(result);
        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult<Response>> Put([FromBody] UpdateQuestionCourseDto dto)
        {
            var result = await _questionCourseService.Update(dto);
            return Ok(result);
        }
    }
}
