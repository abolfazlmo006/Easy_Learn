using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace Easy_Learn.UI.Controllers
{
    [Authorize]
    public class AnswersController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IClient _client;

        public AnswersController(IClient client, IMapper mapper)
        {
            _client = client;
            _mapper = mapper;
        }

        [HttpPost("answer/create/{Id:int}")]
        public async Task<ActionResult> create(GetQuestionCourseDetailVM vM, int Id)
        {
            vM.CreateAnswerQuestionVM.QuestionCourseId = Id;
            var map = _mapper.Map<CreateAnswerQuestionDto>(vM.CreateAnswerQuestionVM);
            _client.HttpClient.DefaultRequestHeaders.Authorization =
                     new AuthenticationHeaderValue("Bearer", User.FindFirstValue(ClaimTypes.NameIdentifier));
            var apiresponse = await _client.AnswerQuestionPOSTAsync(map);
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
                return RedirectToAction("Index","Questions",vM);
            }

            return Redirect($"/Question/{Id}");
        }


        public async Task<ActionResult> Delete(int Id,int? questionId = null)
        {
            _client.HttpClient.DefaultRequestHeaders.Authorization =
                     new AuthenticationHeaderValue("Bearer", User.FindFirstValue(ClaimTypes.NameIdentifier));
            try
            {
                var apiresponse = await _client.AnswerQuestionDELETEAsync(Id);
            }
            catch
            {

            }
            if (questionId != null)
            {
                return Redirect($"Question/{questionId}");
            }
            return Redirect($"/user");
        }

        public async Task<ActionResult> Edit(int Id, int? questionId = null)
        {
            ViewData["questionId"] = questionId;

            _client.HttpClient.DefaultRequestHeaders.Authorization =
                     new AuthenticationHeaderValue("Bearer", User.FindFirstValue(ClaimTypes.NameIdentifier));

            var answer = (await _client.GetForUserAsync()).FirstOrDefault(o => o.Id == Id);

            var map = new UpdateAnswerQuestionVM()
            {
                Id = answer.Id,
                Description = answer.Description
            };

            return View(map);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(UpdateAnswerQuestionVM vM, int? questionId = null)
        {
            _client.HttpClient.DefaultRequestHeaders.Authorization =
                     new AuthenticationHeaderValue("Bearer", User.FindFirstValue(ClaimTypes.NameIdentifier));

            var map = _mapper.Map<UpdateAnswerQuestionDto>(vM);

            var apiresponse = await _client.AnswerQuestionPUTAsync(map);

            if (apiresponse.SuccessFul)
            {
                if (questionId != null)
                {
                    return Redirect($"Question/{questionId}");
                }
                return Redirect($"/user");
            }
            else
            {
                if (apiresponse.Errors != null)
                {
                    ViewBag.Error = apiresponse.Errors;
                }
                else
                {
                    ViewBag.Message = apiresponse.Message;
                }
                return View();
            }
        }
    }
}
