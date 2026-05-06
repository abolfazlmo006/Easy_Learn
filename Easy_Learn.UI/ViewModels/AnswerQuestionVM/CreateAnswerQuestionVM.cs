namespace Easy_Learn.UI.ViewModels.AnswerQuestionVM
{
    public class CreateAnswerQuestionVM
    {
        public int QuestionCourseId { get; set; }
        [Required(ErrorMessage = "این فیلد الزامی است")]

        public string Description { get; set; }

    }
}
