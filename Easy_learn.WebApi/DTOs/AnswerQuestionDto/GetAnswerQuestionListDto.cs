namespace Easy_learn.WebApi.DTOs.AnswerQuestionDto
{
    public class GetAnswerQuestionListDto
    {
        public int Id { get; set; }
        public string Full_Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedTime { get; set; }
        public string? UserName { get; set; }
    }
}
