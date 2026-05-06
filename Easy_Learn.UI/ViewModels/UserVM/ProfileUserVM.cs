namespace Easy_Learn.UI.ViewModels.UserVM
{
    public class ProfileUserVM
    {
        public string Email { get; set; }
        public int CourseCount { get; set; }
        public int CommentCount { get; set; }
        public int QuestionCount { get; set; }
        public int AnswerCount { get; set; }
        public List<CourseListVM>? courses { get; set; }
        public List<GetQuestionCourseListVM>? questions { get; set; }
        public List<GetAnswerQuestionListVM>? answers { get; set; }
        public List<CommentsVM>? comments { get; set; }
    }
}
