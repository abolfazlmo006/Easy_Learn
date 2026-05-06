using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Easy_learn.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FavoriteController : ControllerBase
    {
        private readonly IFavoriteService _favoriteService;

        public FavoriteController(IFavoriteService favoriteService)
        {
            _favoriteService = favoriteService;
        }

        [HttpGet("GetByUser")]
        public async Task<ActionResult<List<string>>> GetByUser()
        {
            var result = await _favoriteService.GetByUser(User.Identity.Name);
            return result;
        }

        [HttpPost("{CourseId}")]
        public async Task<ActionResult<Response>> Add(int CourseId)
        {
            var result = await _favoriteService.Add(CourseId, User.Identity.Name);
            return result;
        }

        [HttpDelete("{CourseId}")]
        public async Task<ActionResult> Delete(int CourseId)
        {
            await _favoriteService.Delete(CourseId, User.Identity.Name);
            return NoContent();
        }
    }
}
