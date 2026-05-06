using Easy_learn.WebApi.DTOs.AnswerQuestionDto;

namespace Easy_learn.WebApi.Contracts.Services
{
    public interface IAnswerQuestionService
    {
        Task Delete(int Id,string UserName);
        Task<Response> Add(CreateAnswerQuestionDto dto, string userName);
        Task<Response> Update(UpdateAnswerQuestionDto dto);
        Task<List<GetAnswerQuestionListDto>> GetAnswersForUser(string userName);
    }
}
