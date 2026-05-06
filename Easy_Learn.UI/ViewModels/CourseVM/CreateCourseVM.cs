global using System.ComponentModel.DataAnnotations;

namespace Easy_Learn.UI.ViewModels.CourseVM
{
    public class CreateCourseVM
    {
        [Required(ErrorMessage = "فیلد نام دوره الزامی است")]
        public string Name { get; set; }
        [Required(ErrorMessage = "فیلد توضیحات کوتاه دوره الزامی است")]
        public string Short_Description { get; set; }
        [Required(ErrorMessage = "فیلد توضیحات دوره الزامی است")]
        public string Description { get; set; }

        public ICollection<string>? Prerequisite { get; set; }
        public int? Price { get; set; }
        [Required(ErrorMessage = "فیلد وضعیت دوره الزامی است")]
        public string Status { get; set; }
        [Required(ErrorMessage = "فیلد سطح دوره الزامی است")]
        public string Level_Course { get; set; }

        public bool IsFree { get; set; }
        [Required(ErrorMessage = "فیلد تصویر دوره الزامی است")]
        public IFormFile Image { get; set; }

        public int CategoryId { get; set; }
        public List<string> TodoItems { get; set; }

    }
}
