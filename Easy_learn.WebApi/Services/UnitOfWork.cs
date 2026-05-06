global using Easy_learn.WebApi.Contracts;
global using Easy_learn.WebApi.Services.Repositories;

namespace Easy_learn.WebApi.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Easy_LearnDbContext _context;

        public UnitOfWork(Easy_LearnDbContext context)
        {
            _context = context;

            User = new UserRepository(_context);
            Teacher = new TeacherRepository(_context);
            Comment = new CommentRepository(_context);
            Course = new CourseRepository(_context);
            Prerequisite = new PrerequisiteRepository(_context);
            CourseEntityUserEntity = new GenericRepository<CourseEntityUserEntity>(_context);
            Video = new VideoRepository(_context);
            Order = new OrderRepository(_context);
            OrderDetail = new OrderDetailRepository(_context);
            Category = new CategoryRepository(_context);
            QuestionCourse = new QuestionCourseRepository(_context);
            AnswerQuestion  = new AnswerQuestionRepository(_context);
            Favorite = new FavoriteRepository(_context);
            Notification = new NotificationRepository(_context);
        }

        public IUserRepository User { get; private set; }
        public ITeacherRepository Teacher { get; private set; }
        public ICommentRepository Comment { get; private set; }
        public ICourseRepository Course { get; private set; }
        public IPrerequisiteRepository Prerequisite { get; private set; }
        public IGenericRepository<CourseEntityUserEntity> CourseEntityUserEntity { get; private set; }
        public IVideoRepository Video { get; private set; }
        public IOrderRepository Order { get; private set; }
        public IOrderDetailRepository OrderDetail { get; private set; }
        public ICategoryRepository Category { get; private set; }
        public IQuestionCourseRepository QuestionCourse { get; private set; }
        public IAnswerQuestionRepository AnswerQuestion { get; private set; }
        public IFavoriteRepository Favorite { get; private set; }
        public INotificationRepository Notification { get; private set; }



        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
