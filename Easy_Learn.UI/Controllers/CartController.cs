global using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace Easy_Learn.UI.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly IClient _client;
        private readonly IMapper _mapper;
        public CartController(IClient client, IMapper mapper)
        {
            _client = client;
            _mapper = mapper;
        }
        [Route("ShowCart")]
        public async Task<ActionResult> ShowCart()
        {
            _client.HttpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", User.FindFirstValue(ClaimTypes.NameIdentifier));
            var apiresponse = await _client.GetOrderAsync();
            var map = _mapper.Map<GetOrderDetailVM>(apiresponse);
            return View(map);
        }

        [HttpPost, Route("AddToCart/{Id:int}")]
        public async Task<ActionResult> AddToCart(int Id)
        {
            _client.HttpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", User.FindFirstValue(ClaimTypes.NameIdentifier));
            var apiresponse = await _client.OrderDetailPOSTAsync(Id);
            return Redirect("/ShowCart");
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int Id)
        {
            _client.HttpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", User.FindFirstValue(ClaimTypes.NameIdentifier));
            await _client.OrderDetailDELETEAsync(Id);
            return Redirect("/ShowCart");
        }


        public async Task<ActionResult> Pay()
        {
            _client.HttpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", User.FindFirstValue(ClaimTypes.NameIdentifier));
            var apiresponse = await _client.BuyAsync();
            return Redirect("/ShowCart");
        }
    }
}
