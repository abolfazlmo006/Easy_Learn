
using Easy_learn.WebApi.DTOs.NotificationDto;

namespace Easy_learn.WebApi.Services.Repositories
{
    public class NotificationRepository : GenericRepository<NotificationEntity>, INotificationRepository
    {
        private readonly Easy_LearnDbContext _context;
        public NotificationRepository(Easy_LearnDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task DeleteAllForUser(string UserId)
        {
            await _context.Notifications.Where(n => n.UserId == UserId).ExecuteDeleteAsync();
        }

        public async Task<List<GetNotificationDto>> GetByUser(string UserId)
        {
            var result = await _context.Notifications.Where(n => n.UserId == UserId).Select(n=> new GetNotificationDto()
            {
                Id = n.Id,
                DateTime = n.DateTime,
                Title = n.Title,
                Message = n.Message
            }).ToListAsync();
            return result;
        }
    }
}
