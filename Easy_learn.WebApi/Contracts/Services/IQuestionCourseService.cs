using Easy_learn.WebApi.DTOs.QuestionCourseDto;

namespace Easy_learn.WebApi.Contracts.Services
{
    public interface IQuestionCourseService
    {
        Task Delete(int Id,string userName);
        Task<Response> Add(CreateQuestionCourseDto dto , string userName);
        Task<Response> Update(UpdateQuestionCourseDto dto);
        Task<List<GetQuestionCourseListDto>> GetQuestionsForCourse(int CourseId);
        Task<GetQuestionCourseDetailDto> GetQuestionDetail(int QuestionId);
        Task<List<GetQuestionCourseListDto>> GetQuestionsForUser(string userName);
    }
}
