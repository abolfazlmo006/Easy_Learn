using Easy_learn.WebApi.DTOs.AnswerQuestionDto;

namespace Easy_learn.WebApi.DTOs.QuestionCourseDto
{
    public class GetQuestionCourseDetailDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Full_Name { get; set; }
        public DateTime CreatedTime { get; set; }
        public List<GetAnswerQuestionListDto>? AnswerQuestions { get; set; }
        public string? UserName { get; set; }
    }
}
