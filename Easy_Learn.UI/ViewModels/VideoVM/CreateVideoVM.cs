namespace Easy_Learn.UI.ViewModels.VideoVM
{
    public class CreateVideoVM
    {
        [Required(ErrorMessage = "این فیلد الزامی است")]
        public IFormFile Video { get; set; }
        [Required(ErrorMessage = "این فیلد الزامی است")]
        public string Title_Video { get; set; }
    }
}
