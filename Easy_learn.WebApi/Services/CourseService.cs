using Easy_learn.WebApi.DTOs.CourseDto.Validators;
using FluentValidation;

namespace Easy_learn.WebApi.Services
{
    public class CourseService : ICourseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<UserEntity> _userManager;
        public CourseService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<UserEntity> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<Response> Add(CreateCourseDto dto ,string UserName)
        {
            var response = new Response();
            var validator = new CreateCourseDtoValidator();
            var validationresult = await validator.ValidateAsync(dto);

            if (!validationresult.IsValid)
            {
                response.errors = validationresult.Errors.Select(e => e.ErrorMessage).ToList();
                response.Message = "عملیات با شکست مواجه شد";
                response.SuccessFul = false;
                return response;
            }
            var map = _mapper.Map<CourseEntity>(dto);
            var TeacherId = await _unitOfWork.User.GetWithIncludeId(UserName, "Teacher");
            if (TeacherId == 0 || TeacherId == null)
            {
                response.Message = "عملیات با شکست مواجه شد";
                response.SuccessFul = false;
                return response;
            }
            map.TeacherId = (int)TeacherId;
            map.CreateTime = DateTime.Now;
            map.UpdateTime = DateTime.Now;
            await _unitOfWork.Course.Add(map);

            foreach (var item in dto.Prerequisite)
            {
                await _unitOfWork.Prerequisite.Add(new PrerequisiteEntity()
                {
                    CourseId= map.Id,
                    Name = item
                });
            }
            response.Message = "عملیات ایجاد دوره با موفقیت انجام شد";
            response.SuccessFul = true;
            return response;
        }

        public async Task Buy(int CourseId , string UserName)
        {
            await _unitOfWork.CourseEntityUserEntity.Add(new CourseEntityUserEntity()
            {
                CourseId = CourseId,
                UserId = (await _userManager.FindByNameAsync(UserName)).Id
            });      
        }

        public async Task Delete(int Id)
        {
            await _unitOfWork.Course.Delete(Id);
        }

        public async Task<CourseDetailDto> Get(int Id , string? UserName)
        {
            var course = await _unitOfWork.Course.Get(Id);
            if (UserName != null)
            {
                var user = await _userManager.FindByNameAsync(UserName);
                course.IsStudent = await _unitOfWork.User.IsStudent(user.Id, Id);
            }
            course.Videos = await _unitOfWork.Video.GetVideosByCourse(Id);
            course.Prerequisites = await _unitOfWork.Prerequisite.GetPrerequisiteForCourse(Id);

            return course;
        }

        public async Task<List<CourseListDto>> GetAll()
        {
            var courses = await _unitOfWork.Course.GetList();
            return courses;
        }

        public async Task<List<CourseDetailDto>> GetByAdmin()
        {
            var courses = await _unitOfWork.Course.GetByAdmin();

            return courses;
        }

        public async Task<List<CourseListDto>> GetByCategory(int CategoryId)
        {
            var courses = await _unitOfWork.Course.GetCourseByCategory(CategoryId);
            return courses;
        }

        public async Task<List<CourseListDto>> GetByTeacher(string UserName)
        {
            var teacherid = await _unitOfWork.User.GetWithIncludeId(UserName, "Teacher");
            var courses = await _unitOfWork.Course.GetCourseByTeacher((int)teacherid);

            return courses;
        }

        public async Task<List<CourseListDto>> GetByUser(string UserName)
        {
            var user = await _userManager.FindByNameAsync(UserName);
            var courses = await _unitOfWork.Course.GetCourseByUser(user.Id);

            return courses;
        }

        public async Task<Response> Update(UpdateCourseDto dto , int Id)
        {
            var response = new Response();
            var validator = new UpdateCourseDtoValidator();
            var validationresult = await validator.ValidateAsync(dto);

            if (!validationresult.IsValid)
            {
                response.errors = validationresult.Errors.Select(e => e.ErrorMessage).ToList();
                response.Message = "عملیات با شکست مواجه شد";
                response.SuccessFul = false;
                return response;
            }
            var course = await _unitOfWork.Course.GetById(Id);
            course.Short_Description = dto.Short_Description;
            course.Price = (int)dto.Price;
            course.UpdateTime = DateTime.Now;
            course.Status = dto.Status;
            course.Level_Course = dto.Level_Course;
            course.CategoryId = dto.CategoryId;
            course.Description = dto.Description;
            course.Image = dto.Image;
            course.IsFree = dto.IsFree;
            course.IsSuccess = false;
            course.Name = dto.Name;

            await _unitOfWork.Course.Update(course);

            await _unitOfWork.Prerequisite.DeleteByCourse(Id);

            foreach (var item in dto.Prerequisite)
            {
                await _unitOfWork.Prerequisite.Add(new PrerequisiteEntity()
                {
                    CourseId = Id,
                    Name = item
                });
            }
            response.Message = "عملیات ویرایش دوره با موفقیت انجام شد";
            response.SuccessFul = true;
            return response;
        }

        public async Task Verify(int CourseId)
        {
            await _unitOfWork.Course.Verify(CourseId);
        }
    }
}
