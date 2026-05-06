global using AutoMapper;
using Easy_Learn.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace Easy_Learn.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMapper _mapper;
        private readonly IClient _client;
        public HomeController(ILogger<HomeController> logger, IMapper mapper, IClient client)
        {
            _logger = logger;
            _mapper = mapper;
            _client = client;
        }

        public async Task<IActionResult> Index()
        {
            var apiresponse = await _client.CourseAllAsync();
            var map = _mapper.Map<List<CourseListVM>>(apiresponse);
            return View(map);
        }

        [Route("Details/{Id:int}")]
        public async Task<ActionResult<CourseDetailVM>> Details(int Id)
        {
            _client.HttpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", User.FindFirstValue(ClaimTypes.NameIdentifier));
            var apiresponse = await _client.CourseGETAsync(Id);
            var map = _mapper.Map<CourseDetailVM>(apiresponse);

            return View(map);
        }

        [Route("Details/{Id:int}/Questions")]
        public async Task<ActionResult> QuestionsCourse(int Id)
        {
            var apiresponse = await _client.GetForCourseAsync(Id);
            var map = _mapper.Map<List<GetQuestionCourseListVM>>(apiresponse);
            ViewData["Id"] = Id;
            return View(map);
        }

        [Authorize]
        public ActionResult RequestTeacher()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> RequestTeacher(RequestForTeacherVM vM)
        {
            if (!ModelState.IsValid)
            {
                return View(vM);
            }

            var map = _mapper.Map<RequestForTeacherDto>(vM);

            _client.HttpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", User.FindFirstValue(ClaimTypes.NameIdentifier));

            await _client.RequestForTeacherAsync(map);

            return RedirectToAction(nameof(Index));
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
