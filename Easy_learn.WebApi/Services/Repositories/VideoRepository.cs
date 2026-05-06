using Easy_learn.WebApi.DTOs.VideoDto;

namespace Easy_learn.WebApi.Services.Repositories
{
    public class VideoRepository : GenericRepository<VideoEntity>, IVideoRepository
    {
        private readonly Easy_LearnDbContext _context;
        public VideoRepository(Easy_LearnDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task Delete(int Id)
        {
            await _context.Videos.Where(x => x.Id == Id).ExecuteDeleteAsync();
        }

        public async Task<List<VideoEntity>> GetVideosByAdmin()
        {
            var videos = await _context.Videos.Where(x => !x.IsSuccess).ToListAsync();
            return videos;
        }

        public async Task<List<VideosDto>> GetVideosByCourse(int CourseId)
        {
            var videos = await _context.Videos.Where(x => x.CourseId == CourseId && x.IsSuccess).Select(v => new VideosDto()
            {
                Id = v.Id,
                Address_Video = v.Address_Video,
                Title_Video = v.Title_Video
            }).ToListAsync();
            return videos;
        }

        public async Task Verify(int Id)
        {
            var video = await GetById(Id);
            video.IsSuccess = true;
            await _context.SaveChangesAsync();
        }
    }
}
