using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy_Learn.Data.Entities
{
    public class CourseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Short_Description { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public int TeacherId { get; set; }
        public TeacherEntity Teacher { get; set; }
        public string Status { get; set; }
        public string Level_Course { get; set; }
        public List<CourseEntityUserEntity>? Students { get; set; }
        public List<VideoEntity>? Videos { get; set; }
        public List<PrerequisiteEntity>? Prerequisites { get; set; }
        public List<CommentEntity>? Comments { get; set; }
        public bool IsSuccess { get; set; } = false;
        public bool IsFree { get; set; }
        public int Length { get; set; }
        public string Image { get; set; }
        public List<OrderDetailEntity>? OrderDetails { get; set; }
        public int CategoryId { get; set; }
        public CategoryEntity Category { get; set; }
        public List<QuestionCourseEntity>? QuestionsCourse { get; set; }
        public List<FavoriteEntity>? Favorites { get; set; }
    }
}
