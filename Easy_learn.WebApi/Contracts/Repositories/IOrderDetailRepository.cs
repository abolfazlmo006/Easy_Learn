namespace Easy_learn.WebApi.Contracts.Repositories
{
    public interface IOrderDetailRepository : IGenericRepository<OrderDetailEntity>
    {
        Task Delete(int Id);
        Task<List<int>> GetCourseId(int orderId);
    }
}
