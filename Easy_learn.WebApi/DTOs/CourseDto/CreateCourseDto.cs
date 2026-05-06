namespace Easy_learn.WebApi.DTOs.CourseDto
{
    public class CreateCourseDto
    {
        public string Name { get; set; }
        public string Short_Description { get; set; }
        public string Description { get; set; }
        public List<string>? Prerequisite { get; set; }
        public int? Price { get; set; }
        public string Status { get; set; }
        public string Level_Course { get; set; }
        public bool IsFree { get; set; }
        public string Image { get; set; }
        public int CategoryId { get; set; }
    }
}
