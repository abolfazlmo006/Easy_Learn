namespace Easy_Learn.UI.ViewModels.AnswerQuestionVM
{
    public class UpdateAnswerQuestionVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "این فیلد الزامیست")]

        public string Description { get; set; }
    }
}
