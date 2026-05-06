using Easy_learn.WebApi.DTOs.AnswerQuestionDto;
using Easy_learn.WebApi.DTOs.CommentDto;
using Easy_learn.WebApi.DTOs.QuestionCourseDto;

namespace Easy_learn.WebApi.DTOs.UserDto
{
    public class ProfileUserDto
    {
        public string Email { get; set; }
        public int CourseCount { get; set; }
        public int CommentCount { get; set; }
        public int QuestionCount { get; set; }
        public int AnswerCount { get; set; }
        public List<CourseListDto>? courses { get; set; }
        public List<GetQuestionCourseListDto>? questions { get; set; }
        public List<GetAnswerQuestionListDto>? answers { get; set; }
        public List<CommentsDto>? comments { get; set; }
    }
}
