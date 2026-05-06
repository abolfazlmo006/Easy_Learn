using Microsoft.Identity.Client;

namespace Easy_learn.WebApi.Services.Repositories
{
    public class FavoriteRepository : GenericRepository<FavoriteEntity>, IFavoriteRepository
    {
        private readonly Easy_LearnDbContext _context;
        public FavoriteRepository(Easy_LearnDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task Delete(string userId, int courseId)
        {
            await _context.Favorites.Where(f => f.UserId == userId && f.CourseId == courseId).ExecuteDeleteAsync();
        }

        public async Task<List<string>> GetByUser(string userId)
        {
            var names = await _context.Favorites.Where(f => f.UserId == userId).Select(f=> f.Course.Name).ToListAsync();
            return names;
        }
    }
}
