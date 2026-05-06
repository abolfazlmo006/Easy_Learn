namespace Easy_Learn.UI.ViewModels.QuestionVM
{
    public class CreateQuestionCourseVM
    {
        public int CourseId { get; set; }
        [Required(ErrorMessage = "این فیلد اجباری است")]
        public string Title { get; set; }
        [Required(ErrorMessage = "این فیلد اجباری است")]
        public string Description { get; set; }

    }
}
