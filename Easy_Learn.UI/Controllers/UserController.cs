using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace Easy_Learn.UI.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IClient _client;
        private readonly IMapper _mapper;
        public UserController(IClient client, IMapper mapper)
        {
            _client = client;
            _mapper = mapper;
        }

        public async Task<ActionResult> Index()
        {
            _client.HttpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", User.FindFirstValue(ClaimTypes.NameIdentifier));
            var apiresponse = await _client.ProfileAsync();
            var map = _mapper.Map<ProfileUserVM>(apiresponse);
            return View(map);
        }

        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ChangePassword(ChangePasswordVM vM)
        {
            var map = _mapper.Map<ChangePasswordDto>(vM);

            _client.HttpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", User.FindFirstValue(ClaimTypes.NameIdentifier));

            var apiresponse = await _client.ChangePasswordAsync(map);

            return RedirectToAction(nameof(Index));
        }
        public ActionResult Edit()
        {
            return View();
        }

        public async Task<ActionResult> Comments()
        {
            _client.HttpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", User.FindFirstValue(ClaimTypes.NameIdentifier));

            var apiresponse = await _client.GetByUserAsync();
            var map = _mapper.Map<List<CommentsVM>>(apiresponse);

            return View(map);
        }

        public async Task<ActionResult> Questions()
        {
            _client.HttpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", User.FindFirstValue(ClaimTypes.NameIdentifier));

            var apiresponse = await _client.GetForUser2Async();
            var apiresponse2 = await _client.GetForUserAsync();

            var map = _mapper.Map<List<GetQuestionCourseListVM>>(apiresponse);
            var map2 = _mapper.Map<List<GetAnswerQuestionListVM>>(apiresponse2);

            return View(new Answers_QuestionsVM { GetAnswers = map2, GetQuestions = map });
        }
        [Route("Noifications")]
        public async Task<ActionResult> Notifications()
        {
            _client.HttpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", User.FindFirstValue(ClaimTypes.NameIdentifier));

            var apiresponse = await _client.GetByUser4Async();

            var map = _mapper.Map<List<GetNotificationVM>>(apiresponse.OrderByDescending(a=> a.Id));

            return View(map);
        }
    }
}
