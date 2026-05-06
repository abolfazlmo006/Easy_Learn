using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace Easy_Learn.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        private readonly IClient _client;
        private readonly IMapper _mapper;
        public HomeController(IClient clinet, IMapper mapper)
        {
            _client = clinet;
            _mapper = mapper;
        }

        // GET: HomeController
        public async Task<ActionResult> Index()
        {
            _client.HttpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", User.FindFirstValue(ClaimTypes.NameIdentifier));
            var apiresponse = await _client.GetByAdmin2Async();
            var map = _mapper.Map<List<CourseDetailVM>>(apiresponse);
            return View(map);
        }

        public async Task<ActionResult> Requests()
        {
            _client.HttpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", User.FindFirstValue(ClaimTypes.NameIdentifier));
            var apiresponse = await _client.GetRequestForTeacherAsync();
            var map = _mapper.Map<List<GetRequestForTeacherVM>>(apiresponse);

            return View(map);   
        }

        public async Task<ActionResult> VerifyRequestTeacher(int Id)
        {
            _client.HttpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", User.FindFirstValue(ClaimTypes.NameIdentifier));
            await _client.VerifyRequestForTeacherAsync(Id);
            return RedirectToAction("Requests");
        }

    }
}
