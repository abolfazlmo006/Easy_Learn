namespace Easy_learn.WebApi.DTOs.Order_OrderDetail_Dto
{
    public class GetOrderListDto
    {
        public int Id { get; set; }
        public int Total_Price { get; set; }
        public DateTime? BuyTime { get; set; }
        public bool Finaly { get; set; }
        public int Count_Course { get; set; }
    }
}
