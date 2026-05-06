using Easy_learn.WebApi.DTOs.AnswerQuestionDto;

namespace Easy_learn.WebApi.Contracts.Repositories
{
    public interface IAnswerQuestionRepository : IGenericRepository<AnswerQuestionEntity>
    {
        Task Delete(int Id);
        Task<List<GetAnswerQuestionListDto>> GetForQuestion(int QuestionCourseId);
        Task<List<GetAnswerQuestionListDto>> GetForUser(string userId);
    }
}
