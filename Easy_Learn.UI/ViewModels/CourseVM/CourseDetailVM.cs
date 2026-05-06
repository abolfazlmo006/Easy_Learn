namespace Easy_Learn.UI.ViewModels.CourseVM
{
    public class CourseDetailVM
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Short_Description { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public ICollection<VideosVM> Videos { get; set; }

        public int Number_Video { get; set; }

        public string Teacher_Name { get; set; }

        public string Status { get; set; }

        public string Level_Course { get; set; }

        public DateTimeOffset CreateTime { get; set; }

        public DateTimeOffset UpdateTime { get; set; }

        public ICollection<CommentsVM> Comments { get; set; }

        public bool IsSuccess { get; set; }

        public bool IsFree { get; set; }

        public bool IsStudent { get; set; }
        public string Image { get; set; }

        public GetCategoryVM Category { get; set; }

        public ICollection<string> Prerequisites { get; set; }

        public CreateCommentVM CreateCommentVM { get; set; }
        public UpdateCommentVM UpdateCommentVM { get; set; }
    }
}
