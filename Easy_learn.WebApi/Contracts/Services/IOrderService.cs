using Easy_learn.WebApi.DTOs.Order_OrderDetail_Dto;

namespace Easy_learn.WebApi.Contracts.Services
{
    public interface IOrderService
    {
        Task<List<GetOrderDetailDto>> GetOrderByUserWithDetail(int OrderId , string UserName);
        Task<List<GetOrderListDto>> GetOrdersByUser(string UserName);
        Task<Response> Buy(string UserName);
        Task<GetOrderDetailDto> GetLastOrderByUserWithIncloude(string UserName);
    }
}
