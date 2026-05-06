using Easy_learn.WebApi.DTOs.QuestionCourseDto;
using Easy_learn.WebApi.DTOs.QuestionCourseDto.Validators;

namespace Easy_learn.WebApi.Services
{
    public class QuestionCourseService : IQuestionCourseService
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private UserManager<UserEntity> _userManager;
        public QuestionCourseService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<UserEntity> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<Response> Add(CreateQuestionCourseDto dto, string userName)
        {
            var response = new Response();

            var validator = new CreateQuestionCourseDtoValidator();
            var validationresult = await validator.ValidateAsync(dto);
            
            if (!validationresult.IsValid)
            {
                response.errors = validationresult.Errors.Select(e=> e.ErrorMessage).ToList();
                response.SuccessFul = false;
                response.Message = "افزودن سوال با شکست مواجه شد";
                return response;
            }
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                response.SuccessFul=false;
                response.Message = "افزودن سوال با شکست مواجه شد";
                return response;
            }
            var map = _mapper.Map<QuestionCourseEntity>(dto);
            map.UserId = user.Id;
            map.CreatedTime = DateTime.Now;

            await _unitOfWork.QuestionCourse.Add(map);

            response.SuccessFul = true;
            response.Message = "افزودن سوال با موفقیت انجام شد";
            return response;
        }

        public async Task Delete(int Id,string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            var question = await _unitOfWork.QuestionCourse.GetById(Id);
            var role = await _userManager.IsInRoleAsync(user, "Admin");
            if (question.UserId == user.Id || role)
            {
                await _unitOfWork.QuestionCourse.Delete(Id);
            }
        }

        public async Task<GetQuestionCourseDetailDto> GetQuestionDetail(int QuestionId)
        {
            var question = await _unitOfWork.QuestionCourse.GetQuestionCourseDetail(QuestionId);
            question.AnswerQuestions = await _unitOfWork.AnswerQuestion.GetForQuestion(question.Id);
            var questionCourseEntity = await _unitOfWork.QuestionCourse.GetById(QuestionId);

            return question;
        }

        public async Task<List<GetQuestionCourseListDto>> GetQuestionsForCourse(int CourseId)
        {
            var questions = await _unitOfWork.QuestionCourse.GetQuestionCourseList(CourseId);
            return questions;
        }

        public async Task<List<GetQuestionCourseListDto>> GetQuestionsForUser(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            var question = await _unitOfWork.QuestionCourse.GetQuestionCourseListForUser(user.Id);
            return question;
        }

        public async Task<Response> Update(UpdateQuestionCourseDto dto)
        {
            var response = new Response();
            var validator = new UpdateQuestionCourseDtoValidator();
            var validationresult = await validator.ValidateAsync(dto);
            if (!validationresult.IsValid)
            {
                response.errors = validationresult.Errors.Select(e => e.ErrorMessage).ToList();
                response.SuccessFul = false;
                response.Message = "ویرایش سوال با شکست مواجه شد";
                return response;
            }

            var question = await _unitOfWork.QuestionCourse.GetById(dto.Id);
            if (question == null)
            {
                response.SuccessFul = false;
                response.Message = "ویرایش سوال با شکست مواجه شد";
                return response;
            }

            question.Description = dto.Description;
            question.Title = dto.Title;
            await _unitOfWork.QuestionCourse.Update(question);

            response.SuccessFul = true;
            response.Message = "ویرایش سوال با موفقیت انجام شد";
            return response;
        }
    }
}
