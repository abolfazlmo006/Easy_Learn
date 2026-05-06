using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy_Learn.Data.Entities
{
    public class CommentEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool Private { get; set; }
        public bool IsSuccess { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? ModifilyTime { get; set; }
        public int CourseId { get; set; }
        public CourseEntity Course { get; set; }
        public string UserId { get; set; }
        public UserEntity User { get; set; }
    }
}
