namespace Easy_learn.WebApi.Services
{
    public class FavoriteService : IFavoriteService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<UserEntity> _userManager;
        public FavoriteService(IUnitOfWork unitOfWork, UserManager<UserEntity> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<Response> Add(int courseId, string userName)
        {
            var response = new Response();
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                response.SuccessFul = false;
                response.Message = "چنین کاربری پیدا نشد";
                return response;
            }
            var favorite = new FavoriteEntity { CourseId = courseId, UserId = user.Id };

            await _unitOfWork.Favorite.Add(favorite);

            response.SuccessFul = true;
            response.Message = "عملیات با موفقیت انجام شد";
            return response;
        }

        public async Task Delete(int courseId, string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            await _unitOfWork.Favorite.Delete(user.Id, courseId);
        }

        public async Task<List<string>> GetByUser(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            var course_Names = await _unitOfWork.Favorite.GetByUser(user.Id);
            return course_Names;
        }
    }
}
