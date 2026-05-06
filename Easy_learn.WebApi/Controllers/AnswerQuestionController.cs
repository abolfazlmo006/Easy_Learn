using Easy_learn.WebApi.DTOs.AnswerQuestionDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Easy_learn.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AnswerQuestionController : ControllerBase
    {
        private readonly IAnswerQuestionService _answerQuestionService;
        public AnswerQuestionController(IAnswerQuestionService answerQuestionService)
        {
            _answerQuestionService = answerQuestionService;
        }

        [HttpGet("GetForUser")]
        public async Task<ActionResult<List<GetAnswerQuestionListDto>>> GetForUser()
        {
            var answers = await _answerQuestionService.GetAnswersForUser(User.Identity.Name);
            return Ok(answers);
        }

        [HttpPost]
        public async Task<ActionResult<Response>> Add([FromBody] CreateAnswerQuestionDto dto)
        {
            var result = await _answerQuestionService.Add(dto, User.Identity.Name);
            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult<Response>> Update([FromBody] UpdateAnswerQuestionDto dto)
        {
            var result = await _answerQuestionService.Update(dto);
            return Ok(result);
        }

        [HttpDelete("{Id:int}")]
        public async Task<ActionResult<Response>> Delete(int Id)
        {
            await _answerQuestionService.Delete(Id,User.Identity.Name);
            return NoContent();
        }
    }
}
