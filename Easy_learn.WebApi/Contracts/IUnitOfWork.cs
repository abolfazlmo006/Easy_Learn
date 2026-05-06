namespace Easy_learn.WebApi.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        public IUserRepository User { get; }
        public ITeacherRepository Teacher { get; }
        public ICommentRepository Comment { get; }
        public ICourseRepository Course { get; }
        public IPrerequisiteRepository Prerequisite { get; }
        public IGenericRepository<CourseEntityUserEntity> CourseEntityUserEntity { get; }
        public IVideoRepository Video { get; }
        public IOrderRepository Order { get; }
        public IOrderDetailRepository OrderDetail { get; }
        public ICategoryRepository Category { get; }
        public IQuestionCourseRepository QuestionCourse { get; }
        public IAnswerQuestionRepository AnswerQuestion { get; }
        public IFavoriteRepository Favorite { get; }
        public INotificationRepository Notification { get; }

    }
}
