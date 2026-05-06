using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy_Learn.Data.Entities
{
    public class FavoriteEntity
    {
        public int CourseId { get; set; }
        public CourseEntity Course { get; set; }
        public string UserId { get; set; }
        public UserEntity User { get; set; }
    }
}
