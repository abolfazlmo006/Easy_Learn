using Easy_learn.WebApi.DTOs.Order_OrderDetail_Dto;

namespace Easy_learn.WebApi.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICourseService _courseService;

        public OrderService(IUnitOfWork unitOfWork, ICourseService courseService)
        {
            _unitOfWork = unitOfWork;
            _courseService = courseService;
        }

        public async Task<Response> Buy(string UserName)
        {
            var response = new Response();
            try
            {
                var order = await _unitOfWork.Order.Buy(UserName);
                var courseIds = await _unitOfWork.OrderDetail.GetCourseId(order.Id);
                foreach (var courseId in courseIds)
                {
                    await _courseService.Buy(courseId, UserName);
                }
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

            response.SuccessFul = true;
            response.Message = "عملیات با موفقیت انجام شد";

            return response;
        }

        public async Task<GetOrderDetailDto> GetLastOrderByUserWithIncloude(string UserName)
        {
            return await _unitOfWork.Order.GetLastOrderByUserWithIncloude(UserName);
        }

        public async Task<List<GetOrderDetailDto>> GetOrderByUserWithDetail(int OrderId, string UserName)
        {
            return await _unitOfWork.Order.GetOrderByUserWithDetail(OrderId, UserName);
        }

        public async Task<List<GetOrderListDto>> GetOrdersByUser(string UserName)
        {
            return await _unitOfWork.Order.GetOrdersByUser(UserName);
        }
    }
}
