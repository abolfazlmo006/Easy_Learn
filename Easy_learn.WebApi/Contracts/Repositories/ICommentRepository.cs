using Easy_learn.WebApi.DTOs.CommentDto;

namespace Easy_learn.WebApi.Contracts.Repositories
{
    public interface ICommentRepository : IGenericRepository<CommentEntity>
    {
        Task Delete(int Id);
        Task<List<CommentsDto>> GetCommentByCourse(int CourseId, string UserName, string Role = null);
        Task<List<CommentsDto>> GetCommentByUser(string UserName);
        Task<List<CommentsDto>> GetCommentByAdmin();
        Task Verify(int CommentId);
    }
}
