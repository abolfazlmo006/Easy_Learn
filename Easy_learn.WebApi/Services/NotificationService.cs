
using Easy_learn.WebApi.DTOs.NotificationDto;

namespace Easy_learn.WebApi.Services
{
    public class NotificationService : INotificationService
    { 
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<UserEntity> _userManager;

        public NotificationService(UserManager<UserEntity> userManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        public async Task DeleteAllForUser(string UserNane)
        {
            var user = await _userManager.FindByNameAsync(UserNane);
            await _unitOfWork.Notification.DeleteAllForUser(user.Id);
        }

        public async Task<List<GetNotificationDto>> GetByUser(string UserNane)
        {
            var user = await _userManager.FindByNameAsync(UserNane);
            var result = await _unitOfWork.Notification.GetByUser(user.Id);
            return result;
        }
    }
}
