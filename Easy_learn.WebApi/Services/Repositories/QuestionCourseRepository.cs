using Easy_learn.WebApi.DTOs.QuestionCourseDto;

namespace Easy_learn.WebApi.Services.Repositories
{
    public class QuestionCourseRepository : GenericRepository<QuestionCourseEntity>, IQuestionCourseRepository
    {
        private readonly Easy_LearnDbContext _context;
        public QuestionCourseRepository(Easy_LearnDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task Delete(int Id)
        {
            await _context.AnswerQuestions.Where(a => a.QuestionCourseId == Id).ExecuteDeleteAsync();
            await _context.QuestionsCourse.Where(q => q.Id == Id).ExecuteDeleteAsync();
        }

        public async Task<GetQuestionCourseDetailDto> GetQuestionCourseDetail(int Id)
        {
            var question = await _context.QuestionsCourse.Where(q => q.Id == Id).Select(q => new GetQuestionCourseDetailDto()
            {
                Id = q.Id,
                CreatedTime = q.CreatedTime,
                Full_Name = q.User.Full_Name,
                Title = q.Title,
                Description = q.Description,
                UserName = q.User.UserName
            }).FirstOrDefaultAsync();

            return question;
        }

        public async Task<List<GetQuestionCourseListDto>> GetQuestionCourseList(int CourseId)
        {
            var questions = await  _context.QuestionsCourse.Where(q => q.CourseId == CourseId).Select(q=> new GetQuestionCourseListDto()
            {
                Id = q.Id,
                CreatedTime = q.CreatedTime,
                Full_Name = q.User.Full_Name,
                Title = q.Title,
                UserName = q.User.UserName,
                AnswerCount = q.AnswersQuestion.Count
            }).ToListAsync();

            return questions;
        }

        public async Task<List<GetQuestionCourseListDto>> GetQuestionCourseListForUser(string UserId)
        {
            var questions = await _context.QuestionsCourse.Where(q => q.UserId == UserId).Select(q => new GetQuestionCourseListDto()
            {
               Id = q.Id,
                CreatedTime = q.CreatedTime,
                Full_Name = q.User.Full_Name,
                Title = q.Title,
                AnswerCount = q.AnswersQuestion.Count
            }).ToListAsync();

            return questions;
        }
    }
}
