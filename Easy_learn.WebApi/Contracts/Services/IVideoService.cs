using Easy_learn.WebApi.DTOs.VideoDto;

namespace Easy_learn.WebApi.Contracts.Services
{
    public interface IVideoService
    {
        Task<List<VideosDto>> GetByCourse(int CourseId);
        Task Delete(int Id);
        Task<Response> Add(CreateVideoDto dto , int CourseId);
        Task<Response> Update(CreateVideoDto dto , int Id);
        Task<List<VideosDto>> GetByAdmin();
        Task Verify(int Id);
    }
}
