namespace Easy_Learn.UI.ViewModels.QuestionVM
{
    public class UpdateQuestionCourseVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "این فیلد الزامیست")]
        public string Title { get; set; }
        [Required(ErrorMessage = "این فیلد الزامیست")]
        public string Description { get; set; }
    }
}
