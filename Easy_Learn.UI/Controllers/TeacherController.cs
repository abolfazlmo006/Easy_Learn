using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace Easy_Learn.UI.Controllers
{
    [Authorize(Roles = "Teacher")]
    public class TeacherController : Controller
    {
        private readonly IClient _client;
        private readonly IMapper _mapper;
        public TeacherController(IClient client, IMapper mapper)
        {
            _client = client;
            _mapper = mapper;
        }

        public async Task<ActionResult> Index()
        {
            _client.HttpClient.DefaultRequestHeaders.Authorization =
                   new AuthenticationHeaderValue("Bearer", User.FindFirstValue(ClaimTypes.NameIdentifier));
            //var apiresponse = await _client.
            return View();
        }
        public async Task<ActionResult> CreateCourse()
        {

            var category = await _client.GetAsync();
            var categorymap = _mapper.Map<List<GetCategoryVM>>(category);

            ViewBag.category = categorymap;

            return View("Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<object> CreateCourse(CreateCourseVM vM)
        {
            var category = await _client.GetAsync();
            var categorymap = _mapper.Map<List<GetCategoryVM>>(category);
            if (!ModelState.IsValid)
            {
                ViewBag.category = categorymap;
                return View("Create", vM);
            }
            try
            {
                var name = Path.GetExtension(vM.Image.FileName).ToLower();
                if (name != ".png" && name != ".jpeg" && name != ".jpg")
                {
                    ModelState.AddModelError("Image", "فرمت های قابل قبول (png,jpg,jpeg)");
                    ViewBag.category = categorymap;
                    return View("Course", vM);
                }

                var map = _mapper.Map<CreateCourseDto>(vM);
                map.Image = Path.GetFileName(vM.Image.FileName);
                map.Prerequisite = vM.TodoItems;
                _client.HttpClient.DefaultRequestHeaders.Authorization =
                   new AuthenticationHeaderValue("Bearer", User.FindFirstValue(ClaimTypes.NameIdentifier));

                var apiresponse = await _client.CoursePOSTAsync(map);
                if (apiresponse.SuccessFul)
                {
                    string filePath2 = Path.Combine(Directory.GetCurrentDirectory(),
                    "wwwroot",
                    "Images",
                    "course",
                    map.Image);
                    using (var stream = new FileStream(filePath2, FileMode.Create))
                    {
                        vM.Image.CopyTo(stream);
                    }

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    if (apiresponse.Errors != null)
                    {
                        ViewBag.error = apiresponse.Errors;
                    }
                    else
                    {
                        ViewBag.Message = apiresponse.Message;
                    }
                    ViewBag.category = categorymap;
                    return View("Create", vM);
                }

            }
            catch
            {
                ViewBag.category = categorymap;
                return View("Create", vM);
            }
        }

        public async Task<ActionResult> Manage()
        {
            _client.HttpClient.DefaultRequestHeaders.Authorization =
                   new AuthenticationHeaderValue("Bearer", User.FindFirstValue(ClaimTypes.NameIdentifier));

            var apiresponse = await _client.GetByTeacherAsync();
            var map = _mapper.Map<List<CourseListVM>>(apiresponse);

            return View(map);
        }

        public async Task<ActionResult> ManageCourse(int Id)
        {
            ViewData["ID"] = Id;
            var apiresponse = await _client.GetByCourse2Async(Id);

            var map = _mapper.Map<List<VideosVM>>(apiresponse);
            return View(map);
        }

        [Route("Teacher/ManageCourse/{Id:int}/CreateVideo")]
        public ActionResult createVideo(int Id)
        {
            ViewData["ID"] = Id;
            return View();
        }

        [HttpPost]
        [Route("Teacher/ManageCourse/{Id:int}/CreateVideo")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> createVideo(int Id, CreateVideoVM vM)
        {
            if (!ModelState.IsValid)
            {
                return View(vM);
            }
            try
            {
                var name = Path.GetExtension(vM.Video.FileName).ToLower();
                if (name != ".mp4" && name != ".mkv")
                {
                    ModelState.AddModelError("Video", "فرمت های قابل قبول (mp4,mkv)");
                    return View(vM);
                }
                _client.HttpClient.DefaultRequestHeaders.Authorization =
                   new AuthenticationHeaderValue("Bearer", User.FindFirstValue(ClaimTypes.NameIdentifier));
                var map = _mapper.Map<CreateVideoDto>(vM);
                map.Address_Video = Path.GetFileName(vM.Video.FileName);

                var apiresponse = await _client.VideoPOSTAsync(Id, map);
                if (apiresponse.SuccessFul)
                {
                    string filePath2 = Path.Combine(Directory.GetCurrentDirectory(),
                    "wwwroot",
                    "Videos",
                    map.Address_Video);
                    using (var stream = new FileStream(filePath2, FileMode.Create))
                    {
                        vM.Video.CopyTo(stream);
                    }
                    return RedirectToAction(nameof(ManageCourse), new { Id = Id });
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
                    return View(vM);
                }

            }
            catch
            {
                return View(vM);
                throw;
            }

        }

        [Route("Teacher/ManageCourse/{Id:int}/DeleteVideo/{VideoId:int}/{Video}")]
        public async Task<ActionResult> DeleteVideo(int Id, int VideoId, string Video)
        {
            _client.HttpClient.DefaultRequestHeaders.Authorization =
                   new AuthenticationHeaderValue("Bearer", User.FindFirstValue(ClaimTypes.NameIdentifier));

            string filePath = Path.Combine(Directory.GetCurrentDirectory(),
                "wwwroot",
                "Videos",
                Video);
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }

            await _client.VideoDELETEAsync(VideoId);

            return RedirectToAction(nameof(ManageCourse), new { Id = Id });
        }

        public async Task<ActionResult> EditCourse(int Id)
        {
            var course = await _client.CourseGETAsync(Id);

            var category = await _client.GetAsync();
            var categorymap = _mapper.Map<List<GetCategoryVM>>(category);
            ViewBag.category = categorymap;
            ViewData["Id"] = Id;
            var map = new UpdateCourseVM
            {

                Description = course.Description,
                Short_Description = course.Short_Description,
                IsFree = course.IsFree,
                Level_Course = course.Level_Course,
                Name = course.Name,
                OldImage = course.Image,
                TodoItems = course.Prerequisites.ToList(),
                Price = course.Price,
                Status = course.Status
            };

            return View(map);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditCourse(int Id, UpdateCourseVM vM)
        {
            var category = await _client.GetAsync();
            var categorymap = _mapper.Map<List<GetCategoryVM>>(category);
            if (!ModelState.IsValid)
            {
                ViewBag.category = categorymap;
                return View(vM);
            }
            try
            {
                if (vM.Image != null)
                {
                    var name = Path.GetExtension(vM.Image.FileName).ToLower();
                    if (name != ".png" && name != ".jpeg" && name != ".jpg")
                    {
                        ModelState.AddModelError("Image", "فرمت های قابل قبول (png,jpg,jpeg)");
                        ViewBag.category = categorymap;
                        return View(vM);
                    }
                }


                var map = _mapper.Map<UpdateCourseDto>(vM);
                if (vM.Image == null)
                {
                    map.Image = vM.OldImage;
                }

                map.Prerequisite = vM.TodoItems;

                _client.HttpClient.DefaultRequestHeaders.Authorization =
                   new AuthenticationHeaderValue("Bearer", User.FindFirstValue(ClaimTypes.NameIdentifier));

                var apiresponse = await _client.CoursePUTAsync(Id, map);

                if (vM.Image != null)
                {
                    map.Image = Path.GetFileName(vM.Image.FileName);
                    string filePath2 = Path.Combine(Directory.GetCurrentDirectory(),
                    "wwwroot",
                    "Images",
                    "course",
                    map.Image);
                    using (var stream = new FileStream(filePath2, FileMode.Create))
                    {
                        vM.Image.CopyTo(stream);
                    }
                    string filePath = Path.Combine(Directory.GetCurrentDirectory(),
                "wwwroot",
                "Images",
                    "course",
                vM.OldImage);
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                }

                if (apiresponse.SuccessFul)
                {
                    return RedirectToAction(nameof(Index));
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
                    ViewBag.category = categorymap;
                    return View(vM);
                }

            }
            catch
            {
                ViewBag.category = categorymap;
                return View(vM);
            }
        }
    }
}
