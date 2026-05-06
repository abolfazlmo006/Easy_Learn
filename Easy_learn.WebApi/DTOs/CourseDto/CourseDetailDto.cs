using Easy_learn.WebApi.DTOs.Category;
using Easy_learn.WebApi.DTOs.CommentDto;
using Easy_learn.WebApi.DTOs.VideoDto;
using System.Reflection.PortableExecutable;

namespace Easy_learn.WebApi.DTOs.CourseDto
{
    public class CourseDetailDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Short_Description { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public List<VideosDto> Videos { get; set; }
        public int Number_Video { get; set; }
        public string Teacher_Name { get; set; }
        public string Status { get; set; }
        public string Level_Course { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public List<CommentsDto>? Comments { get; set; }
        public bool IsSuccess { get; set; }
        public bool IsFree { get; set; }
        public bool IsStudent { get; set; }
        public string Image { get; set; }
        public GetCategoryDto Category { get; set; }
        public List<string>? Prerequisites { get; set; }
    }
}
