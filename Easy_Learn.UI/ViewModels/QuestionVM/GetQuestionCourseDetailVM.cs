global using Easy_Learn.UI.ViewModels.AnswerQuestionVM;

namespace Easy_Learn.UI.ViewModels.QuestionVM
{
    public class GetQuestionCourseDetailVM
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Full_Name { get; set; }

        public DateTimeOffset CreatedTime { get; set; }

        public ICollection<GetAnswerQuestionListVM> AnswerQuestions { get; set; }
        public CreateAnswerQuestionVM CreateAnswerQuestionVM { get; set; }
        public string? UserName { get; set; }
    }
}
