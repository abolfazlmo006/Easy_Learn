namespace Easy_Learn.Data.Entities
{
    public class QuestionCourseEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedTime { get; set; }
        public string UserId { get; set; }
        public UserEntity User { get; set; }
        public int CourseId { get; set; }
        public CourseEntity Course { get; set; }
        public List<AnswerQuestionEntity>? AnswersQuestion { get; set; }
    }
}
