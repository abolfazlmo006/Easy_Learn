using Easy_learn.WebApi.DTOs.QuestionCourseDto;

namespace Easy_learn.WebApi.Contracts.Repositories
{
    public interface IQuestionCourseRepository : IGenericRepository<QuestionCourseEntity>
    {
        Task Delete(int Id);
        Task<List<GetQuestionCourseListDto>> GetQuestionCourseList(int CourseId);
        Task<GetQuestionCourseDetailDto> GetQuestionCourseDetail(int Id);
        Task<List<GetQuestionCourseListDto>> GetQuestionCourseListForUser(string UserId);
    }
}
