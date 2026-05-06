namespace Easy_Learn.UI.ViewModels.CourseVM
{
    public class UpdateCourseVM
    {
        [Required(ErrorMessage = "این فیلد الزامی است")]
        public string Name { get; set; }
        [Required(ErrorMessage = "این فیلد الزامی است")]
        public string Short_Description { get; set; }
        [Required(ErrorMessage = "این فیلد الزامی است")]
        public string Description { get; set; }

        public ICollection<string>? Prerequisite { get; set; }

        public int? Price { get; set; }
        [Required(ErrorMessage = "این فیلد الزامی است")]
        public string Status { get; set; }
        [Required(ErrorMessage = "این فیلد الزامی است")]
        public string Level_Course { get; set; }
        public bool IsFree { get; set; }

        public IFormFile? Image { get; set; }
        [Required(ErrorMessage = "این فیلد الزامی است")]
        public int CategoryId { get; set; }

        public string OldImage { get; set; }
        public List<string> TodoItems { get; set; }

    }
}
