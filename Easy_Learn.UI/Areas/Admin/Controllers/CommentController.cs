using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace Easy_Learn.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CommentController : Controller
    {
        private readonly IClient _client;
        private readonly IMapper _mapper;
        public CommentController(IClient client, IMapper mapper)
        {
            _client = client;
            _mapper = mapper;
        }

        public async Task<ActionResult> Index()
        {
            _client.HttpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", User.FindFirstValue(ClaimTypes.NameIdentifier));
            var apirespone = await _client.GetByAdminAsync();
            var map = _mapper.Map<List<CommentsVM>>(apirespone);
            return View(map);
        }

        public async Task<ActionResult> Delete(int Id)
        {
            _client.HttpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", User.FindFirstValue(ClaimTypes.NameIdentifier));
            await _client.CommentDELETEAsync(Id);

            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> Verify(int Id)
        {
            _client.HttpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", User.FindFirstValue(ClaimTypes.NameIdentifier));
            await _client.VerifyGETAsync(Id);
            return RedirectToAction(nameof(Index));
        }
    }
}
