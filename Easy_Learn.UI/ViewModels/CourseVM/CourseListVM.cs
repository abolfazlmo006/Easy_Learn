namespace Easy_Learn.UI.ViewModels.CourseVM
{
    public class CourseListVM
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Short_Description { get; set; }
        public string Teacher_Name { get; set; }
        public string? Status { get; set; }
        public int Price { get; set; }

        public int Length_Course { get; set; }

        public bool IsFree { get; set; }

        public bool IsSuccess { get; set; }

        public string Image { get; set; }
    }
}
