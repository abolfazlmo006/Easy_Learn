using Easy_learn.WebApi.DTOs.TeacherDto;
using Easy_learn.WebApi.DTOs.TeacherDto.Validators;

namespace Easy_learn.WebApi.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<UserEntity> _userManager;
        public TeacherService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<UserEntity> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task Delete(int Id)
        {
            await _unitOfWork.Teacher.Delete(Id);
        }

        public async Task<List<GetRequestForTeacherDto>> GetRequestForTeacher()
        {
            var requests = await _unitOfWork.Teacher.GetRequestForTeacher();
            return requests;
        }

        public async Task<Response> RequestForTeacher(RequestForTeacherDto dto ,string UseName)
        {
            var response = new Response();
            var validator = new RequestForTeacherDtoValidator();
            var validationResult = await validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                response.errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                response.Message = "عملیات درخواست با شکست مواجه شد";
                response.SuccessFul = false;
                return response;
            }
            var map = _mapper.Map<RequestForTeacherEntity>(dto);
            var user = await _userManager.FindByNameAsync(UseName);

            map.UserId = user.Id;
            await _unitOfWork.Teacher.RequestForTeacher(map);


            response.Message = "عملیات درخواست با موفقیت انجام شد";
            response.SuccessFul = true;
            return response;
        }

        public async Task VerifyRequestForTeacher(int RequestForTeacher_Id)
        {
            var result = await _unitOfWork.Teacher.VerifyRequestForTeacher(RequestForTeacher_Id);
            var user = await _userManager.FindByIdAsync(result);
            await _userManager.AddToRoleAsync(user, "Teacher");
        }
    }
}
