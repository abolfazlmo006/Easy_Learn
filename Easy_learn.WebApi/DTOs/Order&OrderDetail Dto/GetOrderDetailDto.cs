namespace Easy_learn.WebApi.DTOs.Order_OrderDetail_Dto
{
    public class GetOrderDetailDto
    {
        public int Id { get; set; }
        public List<CourseListDto> CourseListDtos { get; set; }
        public int Total_Price { get; set; }
        public DateTime? FinalyTime { get; set; }
    }
}
