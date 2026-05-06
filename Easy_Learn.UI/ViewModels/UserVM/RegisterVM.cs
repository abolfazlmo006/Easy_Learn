namespace Easy_Learn.UI.ViewModels.UserVM
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "این فیلد الزامی است")]
        public string Full_Name { get; set; }
        [Required(ErrorMessage = "این فیلد الزامی است")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "این فیلد الزامی است")]
        public int Age { get; set; }
        [Required(ErrorMessage = "این فیلد الزامی است")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نمی باشد")]
        public string Email { get; set; }
        [Required(ErrorMessage = "این فیلد الزامی است")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
