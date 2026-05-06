using Easy_learn.WebApi.DTOs.VideoDto;

namespace Easy_learn.WebApi.Contracts.Repositories
{
    public interface IVideoRepository : IGenericRepository<VideoEntity>
    {
        Task Delete(int Id);
        Task<List<VideosDto>> GetVideosByCourse(int CourseId);
        Task<List<VideoEntity>> GetVideosByAdmin();
        Task Verify(int Id);
    }
}
