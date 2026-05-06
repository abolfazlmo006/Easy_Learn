using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace Easy_Learn.UI.Controllers
{
    [Authorize]
    public class CommentsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IClient _client;

        public CommentsController(IMapper mapper, IClient client)
        {
            _mapper = mapper;
            _client = client;
        }

        
        public async Task<ActionResult> Delete(int Id ,int? courseId = null)
        {
            _client.HttpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", User.FindFirstValue(ClaimTypes.NameIdentifier));
            try
            {
                await _client.CommentDELETEAsync(Id);
            }
            catch
            {

            }

            if (courseId != null)
            {
                return Redirect($"/Details/{courseId}");
            }

            return Redirect("/user");
        }

        public async Task<ActionResult> Edit(int Id , int? courseId = null)
        {
            ViewData["CourseId"] = courseId;
            ViewData["CommentId"] = Id;

            _client.HttpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", User.FindFirstValue(ClaimTypes.NameIdentifier));

            var comment = (await _client.GetByUserAsync()).FirstOrDefault(o => o.Id == Id);

            var map = new UpdateCommentVM
            {
                Title = comment.Title
            };

            return View(map);
        }
        
        [HttpPost]
        public async Task<ActionResult> Edit(int Id, UpdateCommentVM vM, int? courseId = null)
        {
            var map = _mapper.Map<UpdateCommentDto>(vM);
            _client.HttpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", User.FindFirstValue(ClaimTypes.NameIdentifier));
            var apiresponse = await _client.CommentPUTAsync(Id, map);
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
            }
            if (courseId != null)
            {
                return Redirect($"/Details/{courseId}");
            }

            return Redirect("/user");
        }
    }
}
