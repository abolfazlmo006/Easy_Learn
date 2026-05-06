global using Easy_learn.WebApi.DTOs.CourseDto;

namespace Easy_learn.WebApi.Contracts.Repositories
{
    public interface ICourseRepository : IGenericRepository<CourseEntity>
    {
        Task Delete(int Id);
        Task<List<CourseListDto>> GetList();
        Task<CourseDetailDto> Get(int Id);
        Task<List<CourseDetailDto>> GetByAdmin();
        Task Verify(int CourseId);
        Task<List<CourseListDto>> GetCourseByUser(string UserId);
        Task<List<CourseListDto>> GetCourseByTeacher(int TeacherId);
        Task<List<CourseListDto>> GetCourseByCategory(int CategoryId);

    }
}
