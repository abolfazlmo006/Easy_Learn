global using Easy_Learn.UI.ViewModels.CourseVM;

namespace Easy_Learn.UI.ViewModels.UserVM
{
    public class GetOrderDetailVM
    {
        public int Id { get; set; }

        public ICollection<CourseListVM> CourseListDtos { get; set; }

        public int Total_Price { get; set; }

        public DateTimeOffset? FinalyTime { get; set; }
    }
}
