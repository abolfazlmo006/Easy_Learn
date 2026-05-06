using Easy_learn.WebApi.DTOs.NotificationDto;

namespace Easy_learn.WebApi.Contracts.Services
{
    public interface INotificationService
    {
        Task DeleteAllForUser(string UserNane);
        Task<List<GetNotificationDto>> GetByUser(string UserNane);
    }
}
