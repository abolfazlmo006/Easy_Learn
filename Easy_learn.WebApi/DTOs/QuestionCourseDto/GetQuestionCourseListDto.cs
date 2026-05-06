namespace Easy_learn.WebApi.DTOs.QuestionCourseDto
{
    public class GetQuestionCourseListDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Full_Name { get; set; }
        public DateTime CreatedTime { get; set; }
        public string? UserName { get; set; }
        public int AnswerCount { get; set; }
    }
}
