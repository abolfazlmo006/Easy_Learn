using Easy_learn.WebApi.DTOs.Order_OrderDetail_Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

namespace Easy_learn.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "User")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("GetOrder")]
        public async Task<ActionResult<GetOrderDetailDto>> GetOrder()
        {
            var order = await _orderService.GetLastOrderByUserWithIncloude(User.Identity.Name);
            return Ok(order);
        }

        [HttpGet("GetOrderHistories")]
        public async Task<ActionResult<List<GetOrderListDto>>> GetOrderHistories()
        {
            var orders = await _orderService.GetOrdersByUser(User.Identity.Name);

            return Ok(orders);
        }

        [HttpGet("GetOrderHistoryDetail/{OrderId}")]
        public async Task<ActionResult<List<GetOrderDetailDto>>> GetOrderHistoryDetail(int OrderId)
        {
            var order = await _orderService.GetOrderByUserWithDetail(OrderId , User.Identity.Name);

            return order;
        }

        [HttpPost("Buy")]
        public async Task<ActionResult<Response>> Buy()
        {
            var result = await _orderService.Buy(User.Identity.Name);
            return result;
        }
    }
}
