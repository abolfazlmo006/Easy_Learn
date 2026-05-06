using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy_Learn.Data.Entities
{
    public class TeacherEntity
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public UserEntity User { get; set; }
        public List<CourseEntity>? Courses { get; set; }

    }
}
