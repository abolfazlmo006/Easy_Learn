using Easy_learn.WebApi.DTOs.Order_OrderDetail_Dto;

namespace Easy_learn.WebApi.Services.Repositories
{
    public class OrderRepository : GenericRepository<OrderEntity>, IOrderRepository
    {
        private readonly Easy_LearnDbContext _context;
        public OrderRepository(Easy_LearnDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<OrderEntity> Buy(string UserName)
        {
            var order = await GetLastOrderByUser(UserName);
            order.Finaly = true;
            order.BuyTime = DateTime.Now;
            await Update(order);
            return order;
        }

        public async Task<OrderEntity> GetLastOrderByUser(string UserName)
        {
            var order = await _context.Orders.Include(o=> o.OrderDetails).FirstOrDefaultAsync(o => o.User.UserName == UserName && !o.Finaly);

            if (order == null)
            {
                return null;
            }
            return order;
        }

        public async Task<GetOrderDetailDto> GetLastOrderByUserWithIncloude(string UserName)
        {
            var order = await _context.Orders.Where(o=> !o.Finaly && o.User.UserName == UserName).Select(o => new GetOrderDetailDto()
            {
                Id = o.Id,
                FinalyTime = o.BuyTime,
                Total_Price = o.Total_Price,
                CourseListDtos = o.OrderDetails.Select(od => new CourseListDto()
                {
                    Id = od.Id,
                    Name = od.Course.Name,
                    Price = od.Course.Price,
                    Image = od.Course.Image,
                    IsFree = od.Course.IsFree,
                    IsSuccess = od.Course.IsSuccess,
                    Length_Course = od.Course.Length,
                    Teacher_Name = od.Course.Teacher.User.Full_Name
                }).ToList()
            }).FirstOrDefaultAsync();

            return order;
        }

        public async Task<List<GetOrderDetailDto>> GetOrderByUserWithDetail(int OrderId, string UserName)
        {
            var order = await _context.Orders.Where(o => o.Id == OrderId && o.User.UserName == UserName).Select(o => new GetOrderDetailDto()
            {
                Id = o.Id,
                FinalyTime = o.BuyTime,
                Total_Price = o.Total_Price,
                CourseListDtos =  o.OrderDetails.Select(od => new CourseListDto()
                {
                    Id = od.Id,
                    Name = od.Course.Name,
                    Price = od.Course.Price,
                    IsFree = od.Course.IsFree,
                    IsSuccess = od.Course.IsSuccess,
                    Length_Course = od.Course.Length,
                    Teacher_Name = od.Course.Teacher.User.Full_Name
                }).ToList()
            }).ToListAsync();

            return order;
        }

        public async Task<List<GetOrderListDto>> GetOrdersByUser(string UserName)
        {
            var orders = await _context.Orders.Where(o => o.User.UserName == UserName).Select(o=> new GetOrderListDto()
            {
                BuyTime = o.BuyTime,
                Id = o.Id,
                Count_Course = o.OrderDetails.Count,
                Finaly = o.Finaly,
                Total_Price = o.Total_Price
            }).ToListAsync();

            return orders;
        }
    }
}
