using Microsoft.AspNetCore.Mvc;

namespace Easy_Learn.UI.Controllers
{
    public class CourseController : Controller
    {
        private readonly IClient _client;
        private readonly IMapper _mapper;
        public CourseController(IClient client, IMapper mapper)
        {
            _client = client;
            _mapper = mapper;
        }

        public async Task<ActionResult> Index()
        {
            var apiresponse = await _client.CourseAllAsync();
            var map = _mapper.Map<List<CourseListVM>>(apiresponse);
            return View(map);
        }

        [Route("Category/{Id:int}")]
        public async Task<ActionResult> Category(int Id)
        {
            var apiresponse = await _client.GetByCategoryAsync(Id);

            var map = _mapper.Map<List<CourseListVM>>(apiresponse);
            return View("Index",map);
        }
    }
}
