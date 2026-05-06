using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace Easy_Learn.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class VideoController : Controller
    {
        private readonly IClient _client;
        private readonly IMapper _mapper;
        public VideoController(IClient client, IMapper mapper)
        {
            _client = client;
            _mapper = mapper;
        }

        public async Task<ActionResult> Index()
        {
            _client.HttpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", User.FindFirstValue(ClaimTypes.NameIdentifier));

            var apiresponse = await _client.GetByAdmin3Async();
            var map = _mapper.Map<List<VideosVM>>(apiresponse);

            return View(map);
        }
        public async Task<ActionResult> Verify(int Id)
        {
            _client.HttpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", User.FindFirstValue(ClaimTypes.NameIdentifier));

            await _client.VerifyGET2Async(Id);

            return RedirectToAction("index");
        }
    }
}
