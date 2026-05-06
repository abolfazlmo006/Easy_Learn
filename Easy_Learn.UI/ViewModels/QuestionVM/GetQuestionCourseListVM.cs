namespace Easy_Learn.UI.ViewModels.QuestionVM
{
    public class GetQuestionCourseListVM
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Full_Name { get; set; }

        public DateTimeOffset CreatedTime { get; set; }
        public string? UserName { get; set; }
        public int AnswerCount { get; set; }
    }
}
