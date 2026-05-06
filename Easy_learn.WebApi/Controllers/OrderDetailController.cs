using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Easy_learn.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "User")]
    public class OrderDetailController : ControllerBase
    {
        private readonly IOrderDetailService _orderDetailService;

        public OrderDetailController(IOrderDetailService orderDetailService)
        {
            _orderDetailService = orderDetailService;
        }

        [HttpPost("{CourseId}")]
        public async Task<ActionResult<Response>> AddOrderDetail(int CourseId)
        {
            var result = await _orderDetailService.Add(CourseId, User.Identity.Name);
            return Ok(result);
        }

        [HttpDelete("{Id}")]
        public async Task DeleteOrderDetail(int Id)
        {
            await _orderDetailService.Delete(Id);
        }
    }
}
