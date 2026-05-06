using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace Easy_Learn.UI.Controllers
{
    public class QuestionsController : Controller
    {
        private readonly IClient _client;
        private readonly IMapper _mapper;

        public QuestionsController(IMapper mapper, IClient client)
        {
            _mapper = mapper;
            _client = client;
        }

        [Route("Question/{Id:int}")]
        public async Task<ActionResult> Index(int Id)
        {
            var apiresponse = await _client.QuestionCourseGETAsync(Id);
            var map = _mapper.Map<GetQuestionCourseDetailVM>(apiresponse);
            return View(map);
        }
        [Authorize]
        [Route("Question/Create/{Id:int}")]
        public ActionResult create(int Id)
        {
            ViewData["Id"] = Id;
            return View();
        }

        [Authorize]
        [HttpPost("Question/Create/{Id:int}")]
        public async Task<ActionResult> create(CreateQuestionCourseVM vM, int Id)
        {
            vM.CourseId = Id;
            var map = _mapper.Map<CreateQuestionCourseDto>(vM);
            _client.HttpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", User.FindFirstValue(ClaimTypes.NameIdentifier));
            var apiresponse = await _client.QuestionCoursePOSTAsync(map);
            if (!apiresponse.SuccessFul)
            {
                if (apiresponse.Errors != null)
                {
                    ViewBag.Error = apiresponse.Errors;
                }
                else
                {
                    ViewBag.Message = apiresponse.Message;
                }

                return View(vM);
            }

            return Redirect($"/Details/{vM.CourseId}/questions");
        }

        [Authorize]
        public async Task<ActionResult> Delete(int Id,int? courseId = null)
        {
            _client.HttpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", User.FindFirstValue(ClaimTypes.NameIdentifier));
            try
            {
                await _client.QuestionCourseDELETEAsync(Id);
            }
            catch
            {
                
            }
            if (courseId != null)
            {
                return Redirect($"/Details/{courseId}/Questions");
            }

            return Redirect("/user");
        }

        [Authorize]
        public async Task<ActionResult> Edit(int Id)
        {
            _client.HttpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", User.FindFirstValue(ClaimTypes.NameIdentifier));

            var question = await _client.QuestionCourseGETAsync(Id);

            var map = new UpdateQuestionCourseVM
            {
                Id = question.Id,
                Description = question.Description,
                Title = question.Title
            };

            return View(map);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Edit(UpdateQuestionCourseVM vM)
        {
            var map = _mapper.Map<UpdateQuestionCourseDto>(vM);

            _client.HttpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", User.FindFirstValue(ClaimTypes.NameIdentifier));

            var apiresponse = await _client.QuestionCoursePUTAsync(map);

            if (!apiresponse.SuccessFul)
            {
                if (apiresponse.Errors != null)
                {
                    ViewBag.Error = apiresponse.Errors;
                }
                else
                {
                    ViewBag.Message = apiresponse.Message;
                }

                return View(vM);
            }

            return Redirect($"/Question/{vM.Id}");
        }
    }
}
