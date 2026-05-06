namespace Easy_Learn.UI.ViewModels.AnswerQuestionVM
{
    public class GetAnswerQuestionListVM
    {
        public int Id { get; set; }

        public string Full_Name { get; set; }

        public string Description { get; set; }

        public DateTimeOffset CreatedTime { get; set; }
        public string? UserName { get; set; }
    }
}
