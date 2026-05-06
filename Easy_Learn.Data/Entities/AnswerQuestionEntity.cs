using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy_Learn.Data.Entities
{
    public class AnswerQuestionEntity
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime CreatedTime { get; set; }
        public int QuestionCourseId { get; set; }
        public QuestionCourseEntity QuestionCourse { get; set; }
        public string UserId { get; set; }
        public UserEntity User { get; set; }
    }
}
