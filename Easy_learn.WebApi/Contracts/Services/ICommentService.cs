using Easy_learn.WebApi.DTOs.CommentDto;

namespace Easy_learn.WebApi.Contracts.Services
{
    public interface ICommentService
    {
        Task Delete(int Id , string userName);
        Task<Response> Add(CreateCommentDto dto , string UserName, int CourseId);
        Task<Response> Update(UpdateCommentDto dto , string UserName , int Id);
        Task<List<CommentsDto>> GetByCourse(int CourseId , string? UserName);
        Task<List<CommentsDto>> GetByUser(string UserName);
        Task<List<CommentsDto>> GetByAdmin();
        Task Verify(int CommentId);

    }
}
