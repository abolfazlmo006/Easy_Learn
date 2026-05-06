namespace Easy_learn.WebApi.Services
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<UserEntity> _userManager;

        public OrderDetailService(IUnitOfWork unitOfWork, UserManager<UserEntity> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<Response> Add(int CourseId, string UserName)
        {
            var response = new Response();
            var user = await _userManager.FindByNameAsync(UserName);
            var course = await _unitOfWork.Course.GetById(CourseId);
            if (!course.IsFree)
            {
                var order = await _unitOfWork.Order.GetLastOrderByUser(UserName);
                try
                {
                    if (order == null)
                    {
                        order = await _unitOfWork.Order.Add(new OrderEntity()
                        {
                            UserId = user.Id,
                            Total_Price = 0,
                            Finaly = false
                        });
                    }
                    foreach (var item in order.OrderDetails)
                    {
                        if (item.CourseId == course.Id)
                        {
                            response.Message = "این دوره در سبد خرید شما وجود دارد!!";
                            response.SuccessFul = false;
                            return response;
                        }
                    }
                    var orderdetail = await _unitOfWork.OrderDetail.Add(new OrderDetailEntity()
                    {
                        Course = course,
                        OrderId = order.Id
                    });

                    order.Total_Price += orderdetail.Course.Price;
                    await _unitOfWork.Order.Update(order);

                }
                catch (Exception x)
                {
                    response.errors = new List<string>()
                {
                    x.Message
                };

                    response.Message = "عملیات با شکست مواجه شد";
                    response.SuccessFul = false;
                    return response;
                }

            }
            response.Message = "عملیات با موفقیت انجام شد";
            response.SuccessFul = true;
            return response;
        }

        public async Task Delete(int id)
        {
            await _unitOfWork.OrderDetail.Delete(id);
        }
    }
}
