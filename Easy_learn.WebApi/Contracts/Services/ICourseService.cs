namespace Easy_learn.WebApi.Contracts.Services
{
    public interface ICourseService
    {
        Task Delete(int Id);
        Task<List<CourseListDto>> GetAll();
        Task<CourseDetailDto> Get(int Id,string? UserName);
        Task<Response> Add(CreateCourseDto dto , string UserName);
        Task<Response> Update(UpdateCourseDto dto, int Id);
        Task<List<CourseDetailDto>> GetByAdmin();
        Task Verify(int CourseId);
        Task Buy(int CourseId , string UserName);
        Task<List<CourseListDto>> GetByUser (string UserName);
        Task<List<CourseListDto>> GetByTeacher(string UserName);
        Task<List<CourseListDto>> GetByCategory(int CategoryId);

    }
}
