namespace Easy_Learn.UI.ViewModels.CommentVM
{
    public class CreateCommentVM
    {
        [Required(ErrorMessage = "این فیلد اجباری است")]
        public string Title { get; set; }

        public bool Private { get; set; }
    }
}
