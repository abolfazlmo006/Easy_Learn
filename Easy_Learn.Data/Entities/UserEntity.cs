using Microsoft.AspNetCore.Identity;

namespace Easy_Learn.Data.Entities
{
    public class UserEntity : IdentityUser 
    {
        public string Full_Name { get; set; }
        public int Age { get; set; }
        public TeacherEntity? Teacher { get; set; }
        public List<CourseEntityUserEntity>? Courses { get; set; }
        public RequestForTeacherEntity? RequestForTeacher { get; set; }
        public List<CommentEntity>? Comments { get; set; }
        public List<OrderEntity>? Orders { get; set; }
        public List<QuestionCourseEntity>? QuestionsCourse { get; set; }
        public List<AnswerQuestionEntity>? AnswersQuestion { get; set; }
        public List<FavoriteEntity>? Favorites { get; set; }
        public List<NotificationEntity>? Notifications { get; set; }
    }
}
