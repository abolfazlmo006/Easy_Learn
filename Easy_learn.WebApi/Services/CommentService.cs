using Easy_learn.WebApi.DTOs.CommentDto;
using Easy_learn.WebApi.DTOs.CommentDto.Validators;

namespace Easy_learn.WebApi.Services
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<UserEntity> _userManager;
        public CommentService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<UserEntity> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<Response> Add(CreateCommentDto dto, string UserName, int CourseId)
        {
            var response = new Response();
            var Validator = new CreateCommentDtoValidator();
            var ValidationResult = await Validator.ValidateAsync(dto);

            if (!ValidationResult.IsValid)
            {
                response.errors = ValidationResult.Errors.Select(e => e.ErrorMessage).ToList();
                response.Message = "عملیات افزودن نظر با خطا مواجه شد";
                response.SuccessFul = false;
                return response;
            }

            var map = _mapper.Map<CommentEntity>(dto);

            var user = await _userManager.FindByNameAsync(UserName);
            map.CreateTime = DateTime.Now;
            map.UserId = user.Id;
            map.CourseId = CourseId;
            await _unitOfWork.Comment.Add(map);

            response.Message = "عملیات افزودن نظر با موفقیت انجام شد";
            response.SuccessFul = true;
            return response;
        }

        public async Task Delete(int Id, string userName)
        {
            var comment = await _unitOfWork.Comment.GetById(Id);
            var user = await _userManager.FindByNameAsync(userName);
            var role = await _userManager.IsInRoleAsync(user, "Admin");
            if (comment.UserId == user.Id || role)
            {
                await _unitOfWork.Comment.Delete(Id);
            }
        }

        public async Task<List<CommentsDto>> GetByAdmin()
        {
            return await _unitOfWork.Comment.GetCommentByAdmin();
        }

        public async Task<List<CommentsDto>> GetByCourse(int CourseId, string? UserName)
        {

            if (UserName != null)
            {
                var user = await _userManager.FindByNameAsync(UserName);
                var userRole = await _userManager.GetRolesAsync(user);

                foreach (var item in userRole)
                {
                    if (item == "Teacher")
                    {
                        return await _unitOfWork.Comment.GetCommentByCourse(CourseId, UserName, "Teacher");
                    }
                }
                return await _unitOfWork.Comment.GetCommentByCourse(CourseId, UserName);
            }
            else
            {
                return await _unitOfWork.Comment.GetCommentByCourse(CourseId, UserName);
            }

        }

        public async Task<List<CommentsDto>> GetByUser(string UserName)
        {
            return await _unitOfWork.Comment.GetCommentByUser(UserName);
        }

        public async Task<Response> Update(UpdateCommentDto dto, string UserName, int Id)
        {
            var response = new Response();
            var Validator = new UpdateCommentDtoValidator();
            var ValidationResult = await Validator.ValidateAsync(dto);

            if (!ValidationResult.IsValid)
            {
                response.errors = ValidationResult.Errors.Select(e => e.ErrorMessage).ToList();
                response.Message = "عملیات ویرایش نظر با خطا مواجه شد";
                response.SuccessFul = false;
                return response;
            }
            var comment = await _unitOfWork.Comment.GetById(Id);
            var user = await _userManager.FindByNameAsync(UserName);

            if (comment.UserId != user.Id)
            {
                response.Message = "عملیات ویرایش نظر با خطا مواجه شد";
                response.SuccessFul = false;
                return response;
            }

            comment.Title = dto.Title;
            comment.ModifilyTime = DateTime.Now;
            comment.IsSuccess = false;

            await _unitOfWork.Comment.Update(comment);

            response.Message = "عملیات ویرایش نظر با موفقیت انجام شد";
            response.SuccessFul = true;
            return response;
        }

        public async Task Verify(int CommentId)
        {
            var builder = await _unitOfWork.Comment.GetById(CommentId);
            var product = await _unitOfWork.Course.GetById(builder.CourseId);
            var message = $"نظر برای محصول '<a href=\"/details/{product.Id}\">{product.Name}</a>' توسط ادمین تایید شد";
            await _unitOfWork.Comment.Verify(CommentId);
            await _unitOfWork.Notification.Add(new NotificationEntity()
            {
                UserId = builder.UserId,
                DateTime = DateTime.Now,
                Title = "نظر شما تایید شد",
                Message = message
            });
        }
    }
}
