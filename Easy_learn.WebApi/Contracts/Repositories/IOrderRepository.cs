using Easy_learn.WebApi.DTOs.Order_OrderDetail_Dto;

namespace Easy_learn.WebApi.Contracts.Repositories
{
    public interface IOrderRepository : IGenericRepository<OrderEntity>
    {
        Task<List<GetOrderListDto>> GetOrdersByUser(string UserName);
        Task<List<GetOrderDetailDto>> GetOrderByUserWithDetail(int OrderId , string UserName);
        Task<OrderEntity> GetLastOrderByUser(string UserName);
        Task<OrderEntity> Buy(string UserName);
        Task<GetOrderDetailDto> GetLastOrderByUserWithIncloude(string UserName);
    }
}
