using Easy_learn.WebApi.DTOs.CommentDto;

namespace Easy_learn.WebApi.Services.Repositories
{
    public class CommentRepository : GenericRepository<CommentEntity>, ICommentRepository
    {
        private readonly Easy_LearnDbContext _context;
        public CommentRepository(Easy_LearnDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task Delete(int Id)
        {
            await _context.Comments.Where(c=> c.Id == Id).ExecuteDeleteAsync();
        }

        public async Task<List<CommentsDto>> GetCommentByAdmin()
        {
            return await _context.Comments.Select(c => new CommentsDto()
            {
                Id = c.Id,
                DateTime = (DateTime)(c.ModifilyTime == null ? c.CreateTime : c.ModifilyTime),
                Full_Name = c.User.Full_Name,
                IsSuccess = c.IsSuccess,
                Private = c.Private,
                Title = c.Title,
                CourseId = c.CourseId
            }).AsNoTrackingWithIdentityResolution().Where(c => !c.IsSuccess ).ToListAsync();
        }

        public async Task<List<CommentsDto>> GetCommentByCourse(int CourseId , string UserName , string Role = null )
        {
            return await _context.Comments.Select(c => new CommentsDto()
            {
                Id = c.Id,
                DateTime = (DateTime)(c.ModifilyTime == null ? c.CreateTime:c.ModifilyTime),
                Full_Name = c.User.Full_Name,
                IsSuccess = c.IsSuccess,
                Private = c.Private,
                Title = c.Title,
                CourseId = c.CourseId,
                UserName = c.User.UserName
            }).AsNoTrackingWithIdentityResolution().Where(c=> c.IsSuccess == true && c.CourseId == CourseId && (Role == "Teacher" ? (c.Private || !c.Private) : !c.Private || c.UserName == UserName )).ToListAsync();
        }

        public async Task<List<CommentsDto>> GetCommentByUser(string UserName)
        {
            return await _context.Comments.Select(c => new CommentsDto()
            {
                Id = c.Id,
                DateTime = (DateTime)(c.ModifilyTime == null ? c.CreateTime : c.ModifilyTime),
                Full_Name = c.User.Full_Name,
                IsSuccess = c.IsSuccess,
                Private = c.Private,
                Title = c.Title,
                CourseId = c.CourseId,
                UserName = c.User.UserName
                
            }).AsNoTrackingWithIdentityResolution().Where(c => c.IsSuccess && c.UserName == UserName).ToListAsync();
        }

        public async Task Verify(int CommentId)
        {
            var comment = await GetById(CommentId);

            comment.IsSuccess = true;

            await _context.SaveChangesAsync();
        }
    }
}
