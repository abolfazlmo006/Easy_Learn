using Easy_learn.WebApi.DTOs.AnswerQuestionDto;
using Easy_learn.WebApi.DTOs.AnswerQuestionDto.Validators;

namespace Easy_learn.WebApi.Services
{
    public class AnswerQuestionService : IAnswerQuestionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<UserEntity> _userManager;
        public AnswerQuestionService(IUnitOfWork unitOfWork, UserManager<UserEntity> userManager, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<Response> Add(CreateAnswerQuestionDto dto, string userName)
        {
            var response = new Response();
            var validator = new CreateAnswerQuestionDtoValidator();
            var validationresult = await validator.ValidateAsync(dto);
            if (!validationresult.IsValid)
            {
                response.errors = validationresult.Errors.Select(x => x.ErrorMessage).ToList();
                response.Message = "عملیات ثبت پاسخ سوال با خطا مواجه شد";
                response.SuccessFul = false;
                return response;
            }
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                response.SuccessFul = false;
                response.Message = "عملیات ثبت پاسخ سوال با خطا مواجه شد";
                return response;
            }

            var answerQuestion = _mapper.Map<AnswerQuestionEntity>(dto);
            answerQuestion.UserId = user.Id;
            answerQuestion.CreatedTime = DateTime.Now;
            var builderQuestion = await _unitOfWork.QuestionCourse.GetById(dto.QuestionCourseId);
            await _unitOfWork.AnswerQuestion.Add(answerQuestion);
            if (builderQuestion.UserId != user.Id)
            {
                await _unitOfWork.Notification.Add(new NotificationEntity()
                {
                    UserId = builderQuestion.UserId,
                    DateTime = DateTime.Now,
                    Title = "پاسخ جدیدی برای سوال شما آمده است",
                    Message = $"کاربر '{user.Full_Name}' برای سوال '<a href=\"/Question/{builderQuestion.Id}\">{builderQuestion.Title}</a>' پاسخ داده است"
                });
            }
            response.SuccessFul = true;
            response.Message = "عملیات ثبت پاسخ سوال با موفقیت انجام شد";
            return response;
        }

        public async Task Delete(int Id,string UserName)
        {
            var user = await _userManager.FindByNameAsync(UserName);
            var role = await _userManager.IsInRoleAsync(user, "Admin");
            var answer = await _unitOfWork.AnswerQuestion.GetById(Id);
            if (answer.UserId == user.Id || role)
            {
                await _unitOfWork.AnswerQuestion.Delete(Id);
            }
        }

        public async Task<List<GetAnswerQuestionListDto>> GetAnswersForUser(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            var answers = await _unitOfWork.AnswerQuestion.GetForUser(user.Id);
            return answers;
        }

        public async Task<Response> Update(UpdateAnswerQuestionDto dto)
        {
            var response = new Response();
            var validator = new UpdateAnswerQuestionDtoValidator();
            var validationresult = await validator.ValidateAsync(dto);
            if (!validationresult.IsValid)
            {
                response.errors = validationresult.Errors.Select(e => e.ErrorMessage).ToList();
                response.Message = "عملیات ویرایش پاسخ سوال با شکست مواجه شد";
                response.SuccessFul = false;
                return response;
            }
            var answer = await _unitOfWork.AnswerQuestion.GetById(dto.Id);
            if (answer == null)
            {
                response.Message = "عملیات ویرایش پاسخ سوال با شکست مواجه شد";
                response.SuccessFul = false;
                return response;
            }
            answer.Description = dto.Description;
            await _unitOfWork.AnswerQuestion.Update(answer);

            response.Message = "عملیات ویرایش پاسخ سوال با موفقیت انجام شد";
            response.SuccessFul = true;
            return response;
        }
    }
}
