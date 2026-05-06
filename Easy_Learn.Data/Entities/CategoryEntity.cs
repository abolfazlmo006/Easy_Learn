using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy_Learn.Data.Entities
{
    public class CategoryEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<CourseEntity>? Courses { get; set; }
        public string Description { get; set; }
    }
}
