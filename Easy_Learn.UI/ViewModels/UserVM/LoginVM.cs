using System.ComponentModel.DataAnnotations;

namespace Easy_Learn.UI.ViewModels.UserVM
{
    public class LoginVM
    {
        [Required(ErrorMessage = "این فیلد الزامی است")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "این فیلد الزامی است")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
