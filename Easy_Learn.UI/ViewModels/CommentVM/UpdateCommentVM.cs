namespace Easy_Learn.UI.ViewModels.CommentVM
{
    public class UpdateCommentVM
    {
        [Required(ErrorMessage = "این فیلد الزامی است")]
        public string Title { get; set; }
    }
}
