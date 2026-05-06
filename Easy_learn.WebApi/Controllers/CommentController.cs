using Easy_learn.WebApi.DTOs.CommentDto;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Easy_learn.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        // GET: api/<CommentController>
        [HttpGet("GetByAdmin")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<CommentsDto>>> GetByAdmin()
        {
            var comments = await _commentService.GetByAdmin();
            return Ok(comments);
        }

        // GET api/<CommentController>/5
        [HttpGet("GetByCourse/{CourseId}")]
        public async Task<ActionResult<List<CommentsDto>>> GetByCourse(int CourseId)
        {
            var comments = await _commentService.GetByCourse(CourseId,User.Identity.Name);
            return Ok(comments);
        }

        // POST api/<CommentController>
        [HttpPost("{CourseId}")]
        [Authorize]
        public async Task<ActionResult<Response>> Post(int CourseId, [FromBody] CreateCommentDto dto)
        {
            var result = await _commentService.Add(dto, User.Identity.Name, CourseId);
            return Ok(result);
        }

        // PUT api/<CommentController>/5
        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<Response>> Put(int id, [FromBody] UpdateCommentDto dto)
        {
            var result = await _commentService.Update(dto, User.Identity.Name, id);
            return Ok(result);
        }

        // DELETE api/<CommentController>/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task Delete(int id)
        {
            await _commentService.Delete(id , User.Identity.Name);
        }

        [HttpGet("GetByUser")]
        [Authorize]
        public async Task<ActionResult<List<CommentsDto>>> GetByUser()
        {
            var result = await _commentService.GetByUser(User.Identity.Name);

            return Ok(result);
        }
        [HttpGet("Verify/{CommentId}")]
        [Authorize]
        public async Task<ActionResult> Verify(int CommentId)
        {
            await _commentService.Verify(CommentId);

            return NoContent();
        }
    }
}
