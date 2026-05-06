using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy_Learn.Data.Entities
{
    public class CourseEntityUserEntity
    {
        public int CourseId { get; set; }
        public string UserId { get; set; }
        public CourseEntity Course { get; set; }
        public UserEntity User { get; set; }
    }
}
