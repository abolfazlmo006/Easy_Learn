using Easy_learn.WebApi.DTOs.NotificationDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Easy_learn.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "User")]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet("GetByUser")]
        public async Task<ActionResult<List<GetNotificationDto>>> GetByUser()
        {
            var result = await _notificationService.GetByUser(User.Identity.Name);
            return Ok(result);
        }

        [HttpDelete("DeleteAllForUser")]
        public async Task<ActionResult> DeleteAllForUser()
        {
            await _notificationService.DeleteAllForUser(User.Identity.Name);
            return NoContent();
        }
    }
}
