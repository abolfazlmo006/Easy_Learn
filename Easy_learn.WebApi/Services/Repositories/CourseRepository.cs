global using Easy_Learn.Data;
global using Microsoft.EntityFrameworkCore;
using Easy_learn.WebApi.DTOs.Category;

namespace Easy_learn.WebApi.Services.Repositories
{
    public class CourseRepository : GenericRepository<CourseEntity>, ICourseRepository
    {
        private readonly Easy_LearnDbContext _context;
        public CourseRepository(Easy_LearnDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task Delete(int Id)
        {   
            await _context.Comments.Where(c => c.CourseId == Id).ExecuteDeleteAsync();
            await _context.Videos.Where(c => c.CourseId == Id).ExecuteDeleteAsync();
            await _context.Prerequisites.Where(c => c.CourseId == Id).ExecuteDeleteAsync();
            await _context.Courses.Where(c=> c.Id == Id).ExecuteDeleteAsync();
        }

        public async Task<CourseDetailDto> Get(int Id)
        {
            var course = await _context.Courses.Select(c=> new CourseDetailDto()
            {
                Id = c.Id,
                CreateTime = c.CreateTime,
                Short_Description = c.Short_Description,
                Description = c.Description,
                Price = c.Price,
                IsFree = c.IsFree,
                Level_Course = c.Level_Course,
                Name = c.Name,
                Number_Video = c.Videos.Count,
                Status = c.Status,
                UpdateTime = c.UpdateTime,
                Teacher_Name = c.Teacher.User.Full_Name,
                IsSuccess = c.IsSuccess,
                Image = c.Image,
                Category = new GetCategoryDto()
                {
                    Id = c.CategoryId,
                    Title = c.Category.Title
                }
            }).FirstOrDefaultAsync(c=> c.Id == Id);
            return course;
        }

        public async Task<List<CourseDetailDto>> GetByAdmin()
        {
            var course = await _context.Courses.Select(c => new CourseDetailDto()
            {
                Id = c.Id,
                CreateTime = c.CreateTime,
                Description = c.Description,
                IsFree = c.IsFree,
                Level_Course = c.Level_Course,
                Name = c.Name,
                Number_Video = c.Videos.Count,
                Status = c.Status,
                UpdateTime = c.UpdateTime,
                Teacher_Name = c.Teacher.User.Full_Name,
                IsSuccess = c.IsSuccess,
                Image = c.Image,
                Category = new GetCategoryDto()
                {
                    Id = c.CategoryId,
                    Title = c.Category.Title
                }
            }).Where(c => !c.IsSuccess).ToListAsync();
            return course;
        }

        public async Task<List<CourseListDto>> GetCourseByCategory(int CategoryId)
        {
            var courses = await _context.Courses.Where(c => c.CategoryId == CategoryId && c.IsSuccess).Select(c => new CourseListDto()
            {
                Id = c.Id,
                IsSuccess = c.IsSuccess,
                Image = c.Image,
                Short_Description = c.Short_Description,
                Status = c.Status,
                Name = c.Name,
                IsFree = c.IsFree,
                Price = c.Price,
                Length_Course = c.Length,
                Teacher_Name = c.Teacher.User.Full_Name,
            }).ToListAsync();

            
            return courses;
        }

        public async Task<List<CourseListDto>> GetCourseByTeacher(int TeacherId)
        {
            var courses = await _context.Courses.Where(c => c.TeacherId == TeacherId && c.IsSuccess).Select(c => new CourseListDto()
            {
                Id = c.Id,
                IsSuccess = c.IsSuccess,
                Image = c.Image,
                Short_Description = c.Short_Description,
                Status = c.Status,
                Name = c.Name,
                IsFree = c.IsFree,
                Price = c.Price,
                Length_Course = c.Length,
                Teacher_Name = c.Teacher.User.Full_Name,
            }).ToListAsync();


            return courses;
        }

        public async Task<List<CourseListDto>> GetCourseByUser(string UserId)
        {
            var courses = await _context.CourseEntityUserEntities.Where(c=> c.UserId == UserId).Select(c => new CourseListDto()
            {
                Id = c.CourseId,
                IsSuccess = c.Course.IsSuccess,
                Image = c.Course.Image,
                Name = c.Course.Name,
                IsFree = c.Course.IsFree,
                Price = c.Course.Price,
                Length_Course = c.Course.Length,
                Teacher_Name = c.Course.Teacher.User.Full_Name,
            }).ToListAsync();


            return courses;
        }

        public async Task<List<CourseListDto>> GetList()
        {
            var list = await _context.Courses.Select(c => new CourseListDto
            {
                Id = c.Id,
                Name = c.Name,
                Short_Description = c.Short_Description,
                Status = c.Status,
                IsFree = c.IsFree,
                Length_Course = c.Length,
                Price = c.Price,
                Teacher_Name = c.Teacher.User.Full_Name,
                IsSuccess = c.IsSuccess,
                Image = c.Image
            }).AsNoTrackingWithIdentityResolution().Where(c=> c.IsSuccess).ToListAsync();

            return list;
        }

        public async Task Verify(int CourseId)
        {
            var course = await GetById(CourseId);

            course.IsSuccess = true;

            await _context.SaveChangesAsync();
        }
    }
}
