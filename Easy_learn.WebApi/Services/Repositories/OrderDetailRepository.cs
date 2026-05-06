namespace Easy_learn.WebApi.Services.Repositories
{
    public class OrderDetailRepository : GenericRepository<OrderDetailEntity>, IOrderDetailRepository
    {
        private readonly Easy_LearnDbContext _context;
        public OrderDetailRepository(Easy_LearnDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task Delete(int Id)
        {
            var orderDetail = await _context.OrderDetails.Include(o=> o.Order).SingleOrDefaultAsync(o => o.Id == Id);

            var course = await _context.Courses.FirstOrDefaultAsync(c => c.Id == orderDetail.CourseId);

            orderDetail.Order.Total_Price -= course.Price;

            _context.Orders.Update(orderDetail.Order);

            await _context.SaveChangesAsync();
           
            await _context.OrderDetails.Where(o=> o.Id == Id).ExecuteDeleteAsync();
        }

        public async Task<List<int>> GetCourseId(int orderId)
        {
            var ids = await _context.OrderDetails.Where(o => o.OrderId == orderId).Select(o => o.CourseId).ToListAsync();

            return ids;
        }
    }
}
