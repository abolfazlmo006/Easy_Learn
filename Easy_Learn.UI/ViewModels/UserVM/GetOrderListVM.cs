namespace Easy_Learn.UI.ViewModels.UserVM
{
    public class GetOrderListVM
    {
        public int Id { get; set; }

        public int Total_Price { get; set; }

        public DateTimeOffset? BuyTime { get; set; }

        public bool Finaly { get; set; }

        public int Count_Course { get; set; }
    }
}
