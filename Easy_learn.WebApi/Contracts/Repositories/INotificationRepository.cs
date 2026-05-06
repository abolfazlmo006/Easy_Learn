using Easy_learn.WebApi.DTOs.NotificationDto;

namespace Easy_learn.WebApi.Contracts.Repositories
{
    public interface INotificationRepository : IGenericRepository<NotificationEntity>
    {
        Task DeleteAllForUser(string UserId);
        Task<List<GetNotificationDto>> GetByUser(string UserId);
    }
}
