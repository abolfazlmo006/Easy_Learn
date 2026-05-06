using Easy_learn.WebApi.DTOs.AnswerQuestionDto;

namespace Easy_learn.WebApi.Services.Repositories
{
    public class AnswerQuestionRepository : GenericRepository<AnswerQuestionEntity>, IAnswerQuestionRepository
    {
        private readonly Easy_LearnDbContext _context;
        public AnswerQuestionRepository(Easy_LearnDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task Delete(int Id)
        {
            await _context.AnswerQuestions.Where(a => a.Id == Id).ExecuteDeleteAsync();
        }

        public async Task<List<GetAnswerQuestionListDto>> GetForQuestion(int QuestionCourseId)
        {
            var answers = await _context.AnswerQuestions.Where(a => a.QuestionCourseId == QuestionCourseId).Select(a => new GetAnswerQuestionListDto()
            {
                CreatedTime = a.CreatedTime,
                Id = a.Id,
                Description = a.Description,
                Full_Name = a.User.Full_Name,
                UserName = a.User.UserName
            }).ToListAsync();

            return answers;
        }

        public async Task<List<GetAnswerQuestionListDto>> GetForUser(string userId)
        {
            var answers = await _context.AnswerQuestions.Where(a => a.UserId == userId).Select(a => new GetAnswerQuestionListDto()
            {
                CreatedTime = a.CreatedTime,
                Id = a.Id,
                Description = a.Description,
                Full_Name = a.User.Full_Name
            }).ToListAsync();

            return answers;
        }
    }
}
