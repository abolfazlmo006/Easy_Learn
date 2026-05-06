using Easy_learn.WebApi.DTOs.VideoDto;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Easy_learn.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoController : ControllerBase
    {
        private readonly IVideoService _videoService;

        public VideoController(IVideoService videoService)
        {
            _videoService = videoService;
        }

        // GET api/<VideoController>/5
        [HttpGet("GetByCourse/{CourseId}")]
        public async Task<ActionResult<List<VideosDto>>> GetByCourse(int CourseId)
        {
            var result = await _videoService.GetByCourse(CourseId);
            return Ok(result);
        }

        // POST api/<VideoController>
        [HttpPost("{CourseId}")]
        [Authorize(Roles = "Teacher")]
        public async Task<ActionResult<Response>> Post([FromBody] CreateVideoDto dto , int CourseId)
        {
            var result = await _videoService.Add(dto, CourseId);

            return Ok(result);
        }

        // PUT api/<VideoController>/5
        [HttpPut("{Id}")]
        [Authorize(Roles = "Teacher")]
        public async Task<ActionResult<Response>> Put(int Id, [FromBody] CreateVideoDto dto)
        {
            var result = await _videoService.Update(dto,Id);

            return Ok(result);
        }

        // DELETE api/<VideoController>/5
        [HttpDelete("{Id}")]
        [Authorize(Roles = "Teacher")]
        public async Task Delete(int Id)
        {
            await _videoService.Delete(Id);
        }

        [HttpGet("GetByAdmin")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<VideosDto>>> GetByAdmin()
        {
            var result = await _videoService.GetByAdmin();

            return Ok(result);
        }

        [HttpGet("Verify/{Id}")]
        [Authorize(Roles = "Admin")]
        public async Task Verify(int Id)
        {
            await _videoService.Verify(Id);
        }
    }
}
